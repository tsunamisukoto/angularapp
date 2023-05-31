import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../customer.service';
@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent {
  @Output() customerAdded = new EventEmitter();
  form = new FormGroup({
    firstName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    lastName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    email: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    address: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    mobile: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] })
  })
  constructor(private readonly customerService: CustomerService) { }
  submit() {
    this.form.markAllAsTouched();
    if (!this.form.valid) {
      return;
    }
    this.customerService.addCustomer(this.form.getRawValue()).subscribe(newCustomer => {
      this.customerAdded.emit();
      this.form.reset();
    });
  }
}
