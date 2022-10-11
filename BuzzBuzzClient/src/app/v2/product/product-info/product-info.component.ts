import { Component,  OnInit } from '@angular/core';
import { ActivatedRoute,  NavigationEnd,  Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent implements OnInit {
 products!: Product[];
  productId!:number;
  productName!:string;
  activesort: string = "";
  idSortAscending: boolean = true;
  nameSortAscending: boolean = true;
  priceSortAscending: boolean = true;
  addProductUrl:string = "";
  pageSize = 10;
  page =1 ;
  total:number = 0; 
  isAscending:boolean = true;
  columnName: string= "id";
  customerId!:number;

  constructor(private productService: ProductService, private router: Router,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
    this.router.events.subscribe((val) =>{
      if(val instanceof NavigationEnd && val.url.includes("product-info"))
      {
        this.loadProduct();
      }
    })
  }

  loadProduct(){
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if(id){
      this.productService.getProductsWithFilter(id,(this.page-1) * this.pageSize, this.pageSize,this.isAscending,this.columnName)
      .subscribe((response:{total:number,products:Product[]}) => 
      {
        this.total = response.total;
        this.products = response.products;
        this.customerId = id;
      });
    }else{
      this.router.navigate(["/v2"]);
    }
  }

  setProductId(id: number, name:string){
    this.productId = id;
    this.productName = name;
  }

  OnDelete(id:any){
    if(id){
      this.productService.deleteProduct(id).subscribe(()=>{
          this.loadProduct();
        //this.products = this.products.slice(1,this.products.length);      
      })
    }
  }

  onPageChange(e:any){
    this.page = e;
    this.loadProduct();
  }

  OnSort(columnName: string){
    this.activesort = columnName;
    this.columnName = columnName
    switch (columnName) {
      case 'id':
      default:
        this.idSortAscending = !this.idSortAscending;
        this.isAscending = this.idSortAscending;
        break;
      case 'name':
        this.nameSortAscending = !this.nameSortAscending;
        this.isAscending = this.nameSortAscending;
        break;
      case 'price':
        this.priceSortAscending = !this.priceSortAscending;
        this.isAscending = this.priceSortAscending;
        break;
    }
    this.loadProduct();
    console.log(`sorting on ${columnName}`);
  }

  
  editForm(id:number, customerId:number){
    this.router.navigate([`product/${customerId}/${id}`]);
  }

  newForm()
  {
    this.router.navigate([`product/${this.customerId}`]);
  }



}
