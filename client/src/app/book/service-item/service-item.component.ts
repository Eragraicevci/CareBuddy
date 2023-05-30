import { Component, Input, OnInit } from '@angular/core';
import { Service } from 'src/app/models/service';

@Component({
  selector: 'app-service-item',
  templateUrl: './service-item.component.html',
  styleUrls: ['./service-item.component.css']
})
export class ServiceItemComponent {
  @Input() service?: Service;
}
