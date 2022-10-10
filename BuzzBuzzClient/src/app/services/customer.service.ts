import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  url = environment.url + "customer";
  private customerIdSubject = new Subject<[string,number]>();
  customerId = this.customerIdSubject.asObservable();
  
  constructor(private httpClient: HttpClient) { }

  getAll():Observable<any>{
    return this.httpClient.get(this.url).pipe(
      tap(x => console.log(x)),
    );
  }

  getCustomer(id: number):Observable<any>{
    return this.httpClient.get(`${this.url}/${id}`).pipe(
      tap((x:any) => {console.log(x);this.changeCustomerId(x.name,id);}),
    );;
  }

  changeCustomerId(name:string,id: number) {
    this.customerIdSubject.next([name,id]);
  }
}
