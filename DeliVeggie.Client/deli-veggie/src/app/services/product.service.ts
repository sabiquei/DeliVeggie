import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { ProductDetail } from '../models/product-detail';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private productsUrl = 'products';

  constructor(private http: HttpClient) { 
  }

  getProducts(): Observable<ProductDetail[]> {
    return this.http.get<any>(this.productsUrl)
    .pipe(
      catchError(this.handleError<ProductDetail[]>('getProducts', []))
    );
  }

  getProduct(id: string): Observable<ProductDetail> {
    const url = `${this.productsUrl}/${id}`;
    return this.http.get<ProductDetail>(url)
    .pipe(
      catchError(this.handleError<ProductDetail>(`getProduct id=${id}`))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      // this.log(`${operation} failed: ${error.message}`);
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
