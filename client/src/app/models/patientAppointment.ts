import * as cuid from "cuid";

export interface Appointment{
    id:number;
    serviceName:string;
    price:number;
    capacity:number;
    pictureUrl:string;
    hospital:string;
    type:string;
}


export interface PatientAppointment {
    id: string;
    items: Appointment[];
}

export class PatientAppointment implements PatientAppointment {
    id = cuid();
    items: Appointment[] = [];
}

export interface PatientAppointmentTotals {
    additionalCosts: number;
    subtotal: number;
    total: number;
}