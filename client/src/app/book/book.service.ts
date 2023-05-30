import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServicePagination } from '../models/servicePagination';
import { Service } from '../models/service';
import { Hospital } from '../models/hospitals';
import { ServiceType } from '../models/serviceType';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getServices() {
    return this.http.get<ServicePagination<Service[]>>(this.baseUrl + 'services?pageSize=50')
  }

  getHospitals() {
    return this.http.get<Hospital[]>(this.baseUrl + 'services/hospitals');
  }

  getTypes() {
    return this.http.get<ServiceType[]>(this.baseUrl + 'services/serviceType');
  }
}
