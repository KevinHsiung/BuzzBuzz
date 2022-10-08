import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  url = "https://localhost:44354/api/product"
  constructor(private httpClient: HttpClient) { }

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
