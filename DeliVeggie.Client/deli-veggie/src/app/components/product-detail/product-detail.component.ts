import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDetail } from 'src/app/models/product-detail';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent {
  
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService) {}

  product!: ProductDetail;

  ngOnInit() {
    this.getProduct();
  }

  getProduct(): void {
    const id = this.route.snapshot.paramMap.get(('id'));
    this.productService.getProduct(id ?? "").subscribe(p => this.product = p);
  }
}
