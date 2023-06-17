import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AppointmentType } from '../models/appointmentType';
import { map } from 'rxjs';
import { Booking, BookingToCreate } from '../models/booking';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createOrder(booking: BookingToCreate) {
    return this.http.post<Booking>(this.baseUrl + 'bookings', booking);
  }

  getAppointmentTypes() {
    return this.http.get<AppointmentType[]>(this.baseUrl + 'bookings/appointmentType').pipe(
      map(dm => {
        return dm.sort((a, b) => b.price - a.price)
      })
    )
  }
}
