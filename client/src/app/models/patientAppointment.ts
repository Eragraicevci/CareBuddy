import * as cuid from "cuid";

export interface Appointment {
    id: number;
    serviceName: string;
    price: number;
    pictureUrl: string;
    capacity: number;
    hospital: string;
    type: string;
}


export interface PatientAppointment {
    id: string;
    items: Appointment[];
    clientSecret?: string;
    paymentIntentId?: string;
    appointmentTypeId?: number;
    appointmentTypePrice:number;
}

export class PatientAppointment implements PatientAppointment {
    id = cuid();
    items: Appointment[] = [];
    appointmentTypePrice=0;
}

export interface PatientAppointmentTotals {
    additionalCosts: number;
    subtotal: number;
    total: number;
}