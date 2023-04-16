import { Component, OnInit } from '@angular/core';
import { ProductDetail } from 'src/app/models/product-detail';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'view'];

  constructor(private productService: ProductService) {}

  products: ProductDetail[] = [];

  ngOnInit() {
    this.getProducts();
  }

  getProducts(): void {
    this.productService.getProducts().subscribe(p => this.products = p); 
  }
}
