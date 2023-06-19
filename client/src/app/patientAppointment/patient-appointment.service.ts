import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Appointment, PatientAppointment, PatientAppointmentTotals } from '../models/patientAppointment';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Service } from '../models/service';
import { map, tap } from 'rxjs/operators';
import { AppointmentType } from '../models/appointmentType';


@Injectable({
  providedIn: 'root'
})
export class PatientAppointmentService {
  baseUrl = environment.apiUrl;
  private patientAppointmentSource = new BehaviorSubject<PatientAppointment | null>(null);
  patientAppointmentSource$ = this.patientAppointmentSource.asObservable();
  private patientAppointmentTotalSource = new BehaviorSubject<PatientAppointmentTotals | null>(null);
  patientAppointmentTotalSource$ = this.patientAppointmentTotalSource.asObservable();
  

  constructor(private http: HttpClient) { }

  getPatientAppointment(id: string) {
    return this.http.get<PatientAppointment>(this.baseUrl + 'appointment?id=' + id).subscribe({
      next: patientAppointment => {
        this.patientAppointmentSource.next(patientAppointment);
        this.calculateTotals();
      }
    })
  }


  setPatientAppointment(patientAppointment: PatientAppointment) {
    return this.http.post<PatientAppointment>(this.baseUrl + 'appointment', patientAppointment).subscribe({
      next: patientAppointment => {
        this.patientAppointmentSource.next(patientAppointment);
        this.calculateTotals();
      }
    })
  }


  getCurrentPatientAppointmentValue() {
    return this.patientAppointmentSource.value;
  }




  addItemToPatientAppointment(item: Service | Appointment, capacity = 1) {
    if (this.isService(item)) item = this.mapServiceItemToAppointment(item);

    const patientAppointment = this.getCurrentPatientAppointmentValue() ?? this.createPatientAppointment();
    patientAppointment.items = this.addOrUpdateItem(patientAppointment.items, item, capacity);
    this.setPatientAppointment(patientAppointment);
  }


  private addOrUpdateItem(items: Appointment[], itemToAdd: Appointment, capacity: number): Appointment[] {
    const item = items.find(x => x.id === itemToAdd.id);
    if (item) item.capacity += capacity;
    else {
      itemToAdd.capacity = capacity;
      items.push(itemToAdd);
    }
    return items;
  }


  private createPatientAppointment(): PatientAppointment {
    const patientAppointment = new PatientAppointment();
    localStorage.setItem('patientAppointment_id', patientAppointment.id);
    return patientAppointment;
  }

  deletePatientAppointment(patientAppointment: PatientAppointment) {
    return this.http.delete(this.baseUrl + 'appointment?id=' + patientAppointment.id).subscribe({
      next: () => {
        this.patientAppointmentTotalSource.next(null);
        localStorage.removeItem('appointment_id');
      }
    })
  }

  private mapServiceItemToAppointment(item: Service): Appointment {
    return {
      id: item.id,
      serviceName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      capacity: 0,  
      hospital: item.hospital,
      type: item.serviceType
    }
  }

  private calculateTotals() {
    const patientAppointment = this.getCurrentPatientAppointmentValue();
    if (!patientAppointment) return;
    const subtotal = patientAppointment.items.reduce((a, b) => b.price + a, 0);
    const total = subtotal + patientAppointment.appointmentTypePrice;
    this.patientAppointmentTotalSource.next({ additionalCosts:patientAppointment.appointmentTypePrice, total, subtotal });
  }

  createPaymentIntent() {
    return this.http.post<PatientAppointment>(this.baseUrl + 'payments/' + this.getCurrentPatientAppointmentValue()?.id, {})
      .pipe(
        map(patientAppointment => {
            this.patientAppointmentSource.next(patientAppointment);
        })
      )
  }

  setAdditionalCostPrice(appointmentType: AppointmentType) {
    const appointment = this.getCurrentPatientAppointmentValue();
    if (appointment) {
      
      appointment.appointmentTypePrice= appointmentType.price;
      appointment.appointmentTypeId=appointmentType.id;
      this.setPatientAppointment(appointment);
    }

  }



  private isService(item: Service | Appointment): item is Service {
    return (item as Service).hospital !== undefined;
  }

}