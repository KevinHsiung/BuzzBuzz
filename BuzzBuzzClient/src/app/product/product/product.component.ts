import { compileNgModule } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})


export class ProductComponent implements OnInit {
  @Input() products!: Product[];
  @Input() customerId!: Number;
  productId!:number;
  productName!:string;
  activesort: string = "";
  idSortAscending: boolean = true;
  nameSortAscending: boolean = true;
  priceSortAscending: boolean = true;
  page = 1;
  pageSize = 10;

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
  }

  setProductId(id: number, name:string){
    this.productId = id;
    this.productName = name;
  }

  OnDelete(id:any){
    if(id){
      this.productService.deleteProduct(id).subscribe(()=>{
        this.products = this.products.slice(1,this.products.length);      
      })
    }
  }

  OnSort(columnName: string){
    this.activesort = columnName;
    switch (columnName) {
      case 'id':
      default:
        this.idSortAscending = !this.idSortAscending;
        this.SortProduct(this.idSortAscending, columnName);
        break;
      case 'name':
        this.nameSortAscending = !this.nameSortAscending;
        this.SortProduct(this.nameSortAscending, columnName);
        break;
      case 'price':
      this.priceSortAscending = !this.priceSortAscending;
      this.SortProduct(this.priceSortAscending, columnName);
        break;
    }
    console.log(`sorting on ${columnName}`);
  }

  SortProduct(ascendingDirection: boolean, columnName: string){
    if(ascendingDirection){
      this.products.sort((a:any, b:any) => (a[columnName] < b[columnName] ? -1 : 1));              
    }else{
      this.products.sort((a:any, b:any) => (a[columnName] > b[columnName] ? -1 : 1));              
    }
  }
  editForm(id:number){
    this.router.navigate([`product/${this.customerId}/${id}`]);
  }
  newForm()
  {
    this.router.navigate([`product/${this.customerId}`]);
  }
}
