import { User } from "./user";

export interface BookingToCreate {
    patientAppointmentId: string;
    appointmentTypeId: number;
    info: User;
}

export interface Booking {
    id: number;
    patientUsername: string;
    bookingDate: string;
    info: User;
    appointmentType: string;
    additionalCost: number;
    bookingItems: BookingItem[];
    subtotal: number;
    status: string;
  }

  export interface BookingItem {
    serviceId: number;
    serviceName: string;
    pictureUrl: string;
    price: number;
    capacity: number;
  }