import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Customer } from 'src/app/models/customer';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer-info',
  templateUrl: './customer-info.component.html',
  styleUrls: ['./customer-info.component.scss']
})
export class CustomerInfoComponent implements OnInit {
  customer!: Customer;

  constructor(private router: Router,
    private route:ActivatedRoute,private customerService:CustomerService
    ) { }

  ngOnInit(): void {
    this.GetCustomer();
    // this.router.events.subscribe((val) =>{
    //   if(val instanceof NavigationEnd && val.url.includes("customer-info"))
    //   {
    //     console.log(val);
    //     console.log("firing 2" );
    //     this.GetCustomer();
    //   }
    // })
  }

  GetCustomer(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if(id){
      this.customerService.getCustomer(id)
        .subscribe((customer:Customer) => this.customer = customer);
    }else{
      this.router.navigate(["/v2"]);
    }
  }

  

}
