import { Component, Input } from '@angular/core';
import { CustomerModel } from 'src/models/Customer';

@Component({
  selector: 'app-customer-details[customer]',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})
export class CustomerDetailsComponent {
  @Input() customer!: CustomerModel.DetailedView;
}
