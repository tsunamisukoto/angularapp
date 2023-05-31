import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../customer.service';
import { CustomerModel } from 'src/models/Customer';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers: Array<CustomerModel.Listing> = [];
  loading = false;
  selectedCustomer?: CustomerModel.DetailedView;
  constructor(private readonly customerService: CustomerService) {

  }
  ngOnInit(): void {
    this.loadCustomers();
  }
  loadCustomers() {
    this.loading = true;
    this.customerService.getList().subscribe(x => {
      this.customers = x;
      this.loading = false;
    });
  }
  viewCustomer(id: number) {
    this.customerService.getById({ id }).subscribe(customer => {
      this.selectedCustomer = customer;
    })
  }
}
