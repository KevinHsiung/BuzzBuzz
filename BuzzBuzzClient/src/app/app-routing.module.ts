import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer/customer/customer.component';
import { ProductFormComponent } from './product/product-form/product-form.component';
import { ProductComponent } from './product/product/product.component';

const routes: Routes = [
  { path: '', redirectTo: 'customer', pathMatch: 'full' },
  { path: 'customer/:id', component: CustomerComponent },
  { path: 'products', component: ProductComponent },
  { path: 'product/:customerId', component: ProductFormComponent },
  { path: 'product/:customerId/:id', component: ProductFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

