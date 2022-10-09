import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerInfoComponent } from './v2/customer/customer-info/customer-info.component';
import { CustomerComponent } from './customer/customer/customer.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { ProductFormComponent } from './product/product-form/product-form.component';
import { ProductComponent } from './product/product/product.component';
import { HomeComponent } from './v2/home/home/home.component';
import { ProductInfoComponent } from './v2/product/product-info/product-info.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'customer', component: CustomerComponent },
  { path: 'products', component: ProductComponent },
  { path: 'product/:customerId', component: ProductFormComponent },
  { path: 'product/:customerId/:id', component: ProductFormComponent },
  { path: 'v2', component: HomeComponent,
    children:[
      {
        path: "customer-info/:id",
        component: CustomerInfoComponent
      },
      {
        path: "product-info/:id",
        component: ProductInfoComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

