import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Customer } from 'src/app/models/customer';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  constructor( private router: Router,
    private route:ActivatedRoute, 
    private customerService:CustomerService) { }

  customer!: Customer;
  
  ngOnInit(): void {
    this.GetCustomer();
    this.router.events.subscribe((val) =>{
      if(val instanceof NavigationEnd)
      {
        this.GetCustomer();
      }
    })
  }

  GetCustomer(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.customerService.getCustomer(id)
      .subscribe((customer:Customer) => this.customer = customer);
  }
}
