import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServicePagination } from '../models/servicePagination';
import { Service } from '../models/service';
import { Hospital } from '../models/hospitals';
import { ServiceType } from '../models/serviceType';
import { environment } from 'src/environments/environment';
import { BookParams } from '../models/bookParams';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getServices(bookParams:BookParams) {
    let params = new HttpParams();

    if(bookParams.hospitalId>0) params=params.append('hospitalId', bookParams.hospitalId);
    if(bookParams.typeId) params=params.append('typeId', bookParams.typeId);
    params=params.append('sort', bookParams.sort);
    params=params.append('pageIndex', bookParams.pageNumber);
    params=params.append('pageSize', bookParams.pageSize);
    if (bookParams.search)params=params.append('search', bookParams.search);

    return this.http.get<ServicePagination<Service[]>>(this.baseUrl + 'services', {params});
  }

  getService(id: number) {
    return this.http.get<Service>(this.baseUrl + 'services/' + id);
  }

  getHospitals() {
    return this.http.get<Hospital[]>(this.baseUrl + 'services/hospitals');
  }

  getTypes() {
    return this.http.get<ServiceType[]>(this.baseUrl + 'services/types');
  }
}
