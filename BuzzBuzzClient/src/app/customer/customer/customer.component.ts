import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Customer } from 'src/app/models/customer';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  constructor(private customerService:CustomerService, private router: Router, private route: ActivatedRoute) { }

  title = 'buzzbuzz-coding-exercise';
  allCustomers! :Customer[];
  customer!: Customer;

  
  ngOnInit(): void {
      this.populateDropdown();
      this.GetCustomer();
      this.router.events.subscribe((val) =>{
      if(val instanceof NavigationEnd && val.url.includes("customer"))
      {
        console.log(val);
        this.GetCustomer();
      }
    })
  }

  populateDropdown(){
    this.customerService.getAll().subscribe((customers)=> {
      this.allCustomers = customers;
    });
  }

  GoToCustomer(id:number){
    this.router.navigate([`customer/${id}`]);
  }
  
  
  GetCustomer(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    console.log(`id is ${id}`);
    if(id)
    {
      this.customerService.getCustomer(id)
      .subscribe((customer:Customer) => {
        this.customer = customer;
      });
    }
   
  }


}
