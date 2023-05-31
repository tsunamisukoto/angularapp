import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { CustomerService } from './customer.service';
import { CustomerInput } from 'src/models/CustomerInput';
import { CustomerModel } from 'src/models/Customer';

describe('CustomerService', () => {
  let service: CustomerService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CustomerService]
    });
    service = TestBed.inject(CustomerService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should send a POST request to add a customer', () => {
    const customerInput: CustomerInput.Create = {} as any;
    const expectedResponse: CustomerModel.DetailedView = {} as any;

    service.addCustomer(customerInput).subscribe(response => {
      expect(response).toEqual(expectedResponse);
    });

    const req = httpMock.expectOne('/api/customer/customer.add');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(customerInput);
    req.flush(expectedResponse);
  });

  it('should send a POST request to get a customer by ID', () => {
    const customerInput: CustomerInput.IdOnly = { id: 1 };
    const expectedResponse: CustomerModel.DetailedView = {} as any;

    service.getById(customerInput).subscribe(response => {
      expect(response).toEqual(expectedResponse);
    });

    const req = httpMock.expectOne('/api/customer/customer.getById');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(customerInput);
    req.flush(expectedResponse);
  });

  it('should send a POST request to get a list of customers', () => {
    const expectedResponse: CustomerModel.Listing[] = [{} as any];

    service.getList().subscribe(response => {
      expect(response).toEqual(expectedResponse);
    });

    const req = httpMock.expectOne('/api/customer/customers.getList');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({});
    req.flush(expectedResponse);
  });
});