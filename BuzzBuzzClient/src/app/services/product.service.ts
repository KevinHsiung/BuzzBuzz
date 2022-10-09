import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = "https://localhost:44354/api/product"
  private customerIdSubject = new Subject<number>();
  customerId = this.customerIdSubject.asObservable();

  constructor(private httpClient: HttpClient) { }

  changeCustomerId(id: number) {
    this.customerIdSubject.next(id);
  }

  addProduct(product:any):Observable<any>{
    return this.httpClient.post(this.url, product).pipe(
      tap(x => console.log(x)),
    );
  }

  updateProduct(product:any):Observable<any>{
    return this.httpClient.put(this.url, product).pipe(
      tap(x => console.log(x)),
    );
  }
  getAll():Observable<any>{
    return this.httpClient.get(this.url).pipe(
      tap(x => console.log(x)),
    );
  }

  getAllByCustomerId(id:number):Observable<any>{
    return this.httpClient.get(this.url + "/GetAllByCustomerId/" + id).pipe(      
      tap(x => 
        {
          console.log(x);
          this.changeCustomerId(id);
        }),
    );
  }

  getProductsWithFilter(id:number,skip:number,take:number,isAsc:boolean,column: string):Observable<any>{
    return this.httpClient.get(`${this.url}/GetProductWithFilter?id=${id}&skip=${skip}&take=${take}&isAsc=${isAsc}&column=${column}`).pipe(      
      tap(x => 
        {
          console.log(x);
          this.changeCustomerId(id);
        }),
    );
  }

  getProduct(id:number):Observable<any>{
    return this.httpClient.get(`${this.url}?id=${id}`).pipe(
      tap(x => console.log(x)),
    );
  }

  deleteProduct(id: number){
    return this.httpClient.patch(`${this.url}?id=${id}&delete=true`, null).pipe(
      tap(x => console.log(x)),
    );;
  }
}
