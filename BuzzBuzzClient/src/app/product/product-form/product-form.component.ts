import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ProductService } from 'src/app/services/product.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  isNewProduct: boolean = true;
  productForm = new FormGroup({
    customerId :new FormControl(0),
    id : new FormControl(0),
    name: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
  });

  constructor(private productService:ProductService,
    private route:ActivatedRoute,
    private location: Location
    ) { }
  ngOnInit(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    const customerId = parseInt(this.route.snapshot.paramMap.get('customerId')!, 10);
    if(customerId){
      this.productForm.patchValue({customerId: customerId});
    }
    else{
      alert("Customer Id not found");
    }
    if(id > 0){
      this.isNewProduct = false;
      this.GetProduct(id);
    }else{
      this.isNewProduct = true;
      console.log(this.productForm.value);
      this.productForm.get('name')?.value;
      
    }
  }



  GetProduct(id:number): void {
    this.productService.getProduct(id)
      .subscribe((product:Product) => {
        if(product)
        {
          this.productForm.setValue({
            customerId: product.customerId,
            id: product.id,
            name: product.name,
            price: product.price
          });
        }
      });
  }

  onSubmit(){
    if(this.isNewProduct){
      this.productService.addProduct(this.productForm.value).subscribe(()=>{
        this.location.back();
      });
    }else{
      this.productService.updateProduct(this.productForm.value).subscribe(()=>{        
        this.location.back();
      });;
    }
  }

  goBack(){
    this.location.back();
  }

}
