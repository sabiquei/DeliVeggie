import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,

    MatToolbarModule,
    MatTableModule,
    MatCardModule,
    MatIconModule,
    MatDividerModule,
    MatButtonModule
  ],
  exports: [
    MatToolbarModule,
    MatTableModule,
    MatCardModule,
    MatIconModule,
    MatDividerModule,
    MatButtonModule
  ]
})
export class MaterialModule { }
