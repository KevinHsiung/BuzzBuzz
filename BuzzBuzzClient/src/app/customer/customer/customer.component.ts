import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/models/customer';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  constructor(private customerService:CustomerService) { }

  title = 'buzzbuzz-coding-exercise';
  allCustomers! :Customer[];
  customer!: Customer;

  
  ngOnInit(): void {
    this.populateDropdown();
  }

  populateDropdown(){
    this.customerService.getAll().subscribe((customers)=> {
      this.allCustomers = customers;
    });
  }
  
  
  GetCustomer(id:number): void {
    this.customerService.getCustomer(id)
      .subscribe((customer:Customer) => this.customer = customer);
  }


}
