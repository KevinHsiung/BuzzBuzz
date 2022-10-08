import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Customer } from '../models/customer';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  url = "https://localhost:44354/api/customer"
  constructor(private httpClient: HttpClient) { }

  getAll():Observable<any>{
    return this.httpClient.get(this.url).pipe(
      tap(x => console.log(x)),
    );
  }

  getCustomer(id: number):Observable<any>{
    return this.httpClient.get(`${this.url}/${id}`).pipe(
      tap(x => console.log(x)),
    );;
  }
}
