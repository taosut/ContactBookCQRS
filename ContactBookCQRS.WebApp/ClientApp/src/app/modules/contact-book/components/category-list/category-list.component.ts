import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver, ComponentFactory, ComponentRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from 'app/core/models/Category';
import { CategoryService } from '../../category.service';
import { delay } from 'rxjs/operators';
import { faPlusCircle, faMinusCircle, faEdit } from '@fortawesome/free-solid-svg-icons';
import { CategoryComponent } from '../category/category.component';

@Component({
  selector: 'app-category-list',
  templateUrl: 'category-list.component.html',
  styleUrls: ['./category-list.scss']
})
export class CategoryListComponent implements OnInit {

  categories: Category[];
  createEditCategory = false;
  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;
  faEdit = faEdit;
  categoryToEdit: Category;
  componentRef: any;

  @ViewChild("categoryContainer", { read: ViewContainerRef }) container;

  constructor(
    private categoryService: CategoryService,
    private resolver: ComponentFactoryResolver
  ) { }

  ngOnInit() {
    this.loadCategoryList();
  }

  addEditCategory(category?: Category) {
    this.container.clear();
    const factory = this.resolver.resolveComponentFactory(CategoryComponent);
    this.componentRef = this.container.createComponent(factory);
    this.componentRef.instance.category = category;
    this.componentRef.instance.editMode = category ? true : false;

    this.componentRef.instance.loadCategoryList.subscribe(event => {
      this.destroyAndReload();
    });

    this.componentRef.instance.destroyComponent.subscribe(event => {
      this.destroyAndReload();
    });
  }

  toggle(categoryId: string, e){
    if(e.target.checked) {
      this.getContacts(categoryId);
    }
  }

  destroyAndReload() {
    this.componentRef.destroy();
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

}
