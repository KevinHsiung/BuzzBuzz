import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { Customer } from 'src/app/models/customer';
import { CustomerService } from 'src/app/services/customer.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private customerService:CustomerService, private productService: ProductService, private router:Router) { }
  allCustomers! :Customer[];
  customerLink: string = "customer-info"
  customerId :number = 0;
  dropdownTitle :string= "Select";
  isCustomerActive: boolean= false;
  isProductActive: boolean= false;
  subscription!: Subscription;
  prodSubscription!: Subscription;
  show:boolean = true;
  ngOnInit(): void {
    this.populateDropdown();
    this.subscription = this.customerService.customerId.subscribe(customer => this.onSelect(customer[0],customer[1]));
    this.prodSubscription = this.productService.customerId.subscribe(customer => this.onProdSelect(customer));
    this.router.events.subscribe((val:any) =>{
      if(val instanceof NavigationEnd && val.url == "/v2")
      {
        this.show = true;
        this.dropdownTitle = "Select";
        this.isCustomerActive = false;
        this.isProductActive = false;
        this.customerId = 0;

      }
    })
  }

  onProdSelect(id: number){
    this.show = false;
    this.dropdownTitle = this.allCustomers.filter(x=> x.id == id)[0].name;
    this.customerId = id;
    this.isCustomerActive = false;
    this.isProductActive = true;
  }

  populateDropdown(){
    this.customerService.getAll().subscribe((customers)=> {
      this.allCustomers = customers;
    });
  }

  onSelect(name:string, id: number){
    this.show = false;
    this.dropdownTitle = name;
    this.customerId = id;
    this.isCustomerActive = true;
    this.isProductActive = false;
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
    this.prodSubscription.unsubscribe();
  }

  // GoToCustomerDetails(id){
  //   return null;
  // }
}
