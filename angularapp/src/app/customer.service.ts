import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomerModel } from 'src/models/Customer';
import { CustomerInput } from 'src/models/CustomerInput';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private readonly http: HttpClient) { }

  addCustomer(input: CustomerInput.Create) {
    return this.http.post<CustomerModel.DetailedView>('/api/customer/customer.add', input);
  }
  getById(input: CustomerInput.IdOnly) {
    return this.http.post<CustomerModel.DetailedView>('/api/customer/customer.getById', input);
  }
  getList() {
    return this.http.post<Array<CustomerModel.Listing>>('/api/customer/customers.getList', {});
  }
}
