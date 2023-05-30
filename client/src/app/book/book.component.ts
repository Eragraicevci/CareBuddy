import { Component, OnInit } from '@angular/core';
import { Service } from '../models/service';
import { BookService } from './book.service';
import { Hospital } from '../models/hospitals';
import { ServiceType } from '../models/serviceType';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  services: Service[] = [];
  hospitals: Hospital[] = [];
  serviceType: ServiceType[] = [];



  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getServices();
    this.getHospitals();
    this.getTypes();
  }


  getServices() {
    this.bookService.getServices().subscribe({
      next: response => this.services = response.data,
      error: error => console.log(error)
    })
  }

  getHospitals() {
    this.bookService.getHospitals().subscribe({
      next: response => this.hospitals = response,
      error: error => console.log(error)
    })
  }

  getTypes() {
    this.bookService.getTypes().subscribe({
      next: response => this.serviceType = response,
      error: error => console.log(error)
    })
  }

}
