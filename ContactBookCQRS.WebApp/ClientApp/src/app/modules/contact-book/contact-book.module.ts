import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryService } from './category.service';
import { SharedModule } from 'app/shared/shared.module';
import { CreateCategoryComponent } from './components/create-category/create-category.component';

@NgModule({
  declarations: [
    HomeComponent,
    CategoryListComponent,
    CreateCategoryComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  providers: [
    CategoryService
  ]
})
export class ContactBookModule { }
