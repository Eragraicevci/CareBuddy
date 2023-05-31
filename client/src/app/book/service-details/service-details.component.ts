import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';
import { Service } from 'src/app/models/service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-service-details',
  templateUrl: './service-details.component.html',
  styleUrls: ['./service-details.component.css']
})
export class ServiceDetailsComponent implements OnInit {
  service?: Service;

  constructor(private bookService: BookService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadService();
  }

  loadService() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.bookService.getService(+id).subscribe({
      next: service => this.service = service,
      error: error => console.log(error)
    })
  }
}
