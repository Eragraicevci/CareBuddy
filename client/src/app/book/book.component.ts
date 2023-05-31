import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Service } from '../models/service';
import { BookService } from './book.service';
import { Hospital } from '../models/hospitals';
import { ServiceType } from '../models/serviceType';
import { BookParams } from '../models/bookParams';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  services: Service[] = [];
  hospitals: Hospital[] = [];
  serviceType: ServiceType[] = [];
  bookParams = new BookParams();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to high', value: 'priceAsc'},
    {name: 'Price: High to low', value: 'priceDesc'},
  ];
  totalCount=0;


  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getServices();
    this.getHospitals();
    this.getTypes();
  }


  getServices() {
    this.bookService.getServices(this.bookParams).subscribe({
      next: response => {
        this.services = response.data;
        this.bookParams.pageNumber=response.pageIndex;
        this.bookParams.pageSize=response.pageSize;
        this.totalCount=response.count;
      },
      error: error => console.log(error)
    })
  }

  getHospitals() {
    this.bookService.getHospitals().subscribe({
      next: response => this.hospitals = [{id: 0,name:'All'},...response],
      error: error => console.log(error)
    })
  }

  getTypes() {
    this.bookService.getTypes().subscribe({
      next: response => this.serviceType = [{id: 0,name:'All'},...response],
      error: error => console.log(error)
    })
  }

  onHospitalSelected(hospitalId: number) {
    this.bookParams.hospitalId=hospitalId;
    this.bookParams.pageNumber=1;
    this.getServices();
  }

  onTypeSelected(typeId: number) {
    this.bookParams.typeId=typeId;
    this.bookParams.pageNumber=1;
    this.getServices();
  }

  onSortSelected(event: any) {
    this.bookParams.sort = event.target.value;
    this.getServices();
  }

  onPageChanged(event: any) {
    if (this.bookParams.pageNumber !== event) {
      this.bookParams.pageNumber = event;
      this.getServices();
    }
  }

  onSearch() {
    this.bookParams.search = this.searchTerm?.nativeElement.value;
    this.bookParams.pageNumber=1;
    this.getServices();
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.bookParams = new BookParams();
    this.getServices();
  }
}
