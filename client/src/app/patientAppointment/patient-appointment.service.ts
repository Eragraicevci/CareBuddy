import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Appointment, PatientAppointment, PatientAppointmentTotals } from '../models/patientAppointment';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Service } from '../models/service';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PatientAppointmentService {
  baseUrl = environment.apiUrl;
  private patientAppointmentSource = new BehaviorSubject<PatientAppointment | null>(null);
  patientAppointmentSource$ = this.patientAppointmentSource.asObservable();
  private patientAppointmentTotalSource= new BehaviorSubject<PatientAppointmentTotals | null>(null);
  patientAppointmentTotalSource$ = this.patientAppointmentTotalSource.asObservable();
  
  constructor(private http: HttpClient) { }

  getPatientAppointment(id: string) {
    return this.http.get<PatientAppointment>(this.baseUrl + 'patientAppointment?id=' + id).subscribe({
      next: patientAppointment => {
        this.patientAppointmentSource.next(patientAppointment);
        this.calculateTotals();
      }
    })
  }

    setPatientAppointment(patientAppointment: PatientAppointment) {
      return this.http.post<PatientAppointment>(this.baseUrl + 'patientAppointment', patientAppointment).subscribe({
        next: patientAppointment =>{
           this.patientAppointmentSource.next(patientAppointment);
        this.calculateTotals();
          }
        
      })
  }

  getCurrentPatientAppointmentValue() {
    return this.patientAppointmentSource.value;
  }

  addItemToPatientAppointment(item: Service | Appointment) {
    if (this.isService(item)) item= this.mapServiceItemToAppointment(item);
    
    const patientAppointment = this.getCurrentPatientAppointmentValue() ?? this.createPatientAppointment();
    patientAppointment.items = this.addOrUpdateItem(patientAppointment.items, item);
    this.setPatientAppointment(patientAppointment);
  }

  removeAppointmentFromPatientAppointment(id: number) {
    const patientAppointment = this.getCurrentPatientAppointmentValue();
    if (!patientAppointment) return;
    const item = patientAppointment.items.find(x => x.id === id);
   /* if (item) {
      item.quantity -= quantity;
      if (item.quantity === 0) {
        patientAppointment.items = patientAppointment.items.filter(x => x.id !== id);
      }*/
      if (patientAppointment.items.length > 0) this.setPatientAppointment(patientAppointment);
      else this.deletePatientAppointment(patientAppointment);
    }

    deletePatientAppointment(patientAppointment: PatientAppointment) {
      return this.http.delete(this.baseUrl + 'patientAppointment?id=' + patientAppointment.id).subscribe({
        next: () => {
          this.patientAppointmentSource.next(null);
          this.patientAppointmentTotalSource.next(null);
          localStorage.removeItem('patientAppointment_id');
        }
      })
    }
  

  private mapServiceItemToAppointment(item: Service): Appointment {
    return {
      id: item.id,
      serviceName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      hospital: item.hospital,
      type: item.serviceType
    }
  }

    private createPatientAppointment(): PatientAppointment {
      const patientAppointment = new PatientAppointment();
      localStorage.setItem('patientAppointment_id', patientAppointment.id);
      return patientAppointment;
    }

    private addOrUpdateItem(items: Appointment[], itemToAdd: Appointment): Appointment[] {
      const item = items.find(x => x.id === itemToAdd.id);
    //  if (item) item.quantity += quantity;
    //  else {
       // itemToAdd.quantity = quantity;
        items.push(itemToAdd);
    //  }
      return items;
    }

    private calculateTotals() {
      const patientAppointment = this.getCurrentPatientAppointmentValue();
      if (!patientAppointment) return;
      const additionalCosts = 0;
      const subtotal = patientAppointment.items.reduce((a, b) => b.price + a, 0);
      const total = subtotal + additionalCosts;
      this.patientAppointmentTotalSource.next({additionalCosts, total, subtotal});
    }

    private isService(item: Service | Appointment): item is Service {
      return (item as Service).hospital !== undefined;
    }

  }