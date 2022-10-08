import { Component } from '@angular/core';
import { CustomerService } from './services/customer.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'buzzbuzz-coding-exercise';
  customers :any;
  show = false;
  constructor(private customerService: CustomerService) {}

  ngOnInit(){
    this.populateDropdown();
  }

  populateDropdown(){
    this.customerService.getAll().subscribe((customers)=> {
      this.customers = customers;
      this.show = true;
    });
  }
  hide(){
    this.show = false;
  }
}
