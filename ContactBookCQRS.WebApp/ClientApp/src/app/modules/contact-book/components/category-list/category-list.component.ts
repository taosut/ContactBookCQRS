import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from 'app/core/models/Category';
import { CategoryService } from '../../category.service';
import { delay } from 'rxjs/operators';
import { faPlusCircle, faMinusCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-category-list',
  templateUrl: 'category-list.component.html',
  styleUrls: ['./category-list.scss']
})
export class CategoryListComponent implements OnInit {
  public categories: Category[];
  public newCategory = false;
  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;

  constructor(
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.loadCategoryList();
  }

  loadCategoryList() {
    this.categoryService.getCategories()
    //.pipe(delay(1000))
    .subscribe((result: any) => {
      this.categories = result.data;
    },
    error => console.error(error));
  }

  toggle(categoryId: string, e){
    if(e.target.checked) {
      this.getContacts(categoryId);
    }
  }

  getContacts(categoryId: string) {
    var category = this.categories.filter((category : Category) => category.id === categoryId);

    if(category) {
      this.categoryService.getContacts(categoryId)
      //.pipe(delay(1000))
      .subscribe((result: any) => {
        category[0].contacts = result.data;
      },
      error => console.error(error));
    }
  }

  enableCreateCategory(){
    this.newCategory = true;
  }

  deleteCategory(category: Category){
    if(category) {
      this.categoryService.deleteCategory(category.id)
      //.pipe(delay(1000))
      .subscribe((result: any) => {
        this.loadCategoryList();
      },
      error => console.error(error));
    }
  }

  cancelAddCategory(){
    this.newCategory = false;
  }
}
