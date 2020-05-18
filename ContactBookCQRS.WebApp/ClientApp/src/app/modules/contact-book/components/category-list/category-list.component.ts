import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver, ComponentFactory, ComponentRef, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from 'app/core/models/Category';
import { CategoryService } from '../../category.service';
import { faPlusCircle, faMinusCircle, faEdit, faUser } from '@fortawesome/free-solid-svg-icons';
import { CategoryComponent } from '../category/category.component';
import { Contact } from 'app/core/models/Contact';
import { ContactComponent } from '../contact/contact.component';

@Component({
  selector: 'app-category-list',
  templateUrl: 'category-list.component.html',
  styleUrls: ['./category-list.scss']
})
export class CategoryListComponent implements OnInit {

  @ViewChild("categoryContainer", { read: ViewContainerRef }) categoryContainer;
  @ViewChild("contactContainer", { read: ViewContainerRef }) contactContainer;

  categories: Category[];
  categoryToEdit: Category;
  categoryComponentRef: any;
  contactComponentRef: any;
  createEditCategory = false;

  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;
  faEdit = faEdit;
  faUser = faUser;


  constructor(
    private categoryService: CategoryService,
    private resolver: ComponentFactoryResolver
  ) { }

  ngOnInit() {
    this.loadCategoryList();
  }

  addEditCategory(category?: Category) {
    this.categoryContainer.clear();
    const factory = this.resolver.resolveComponentFactory(CategoryComponent);
    this.categoryComponentRef = this.categoryContainer.createComponent(factory);
    this.categoryComponentRef.instance.category = category;
    this.categoryComponentRef.instance.isEditMode = category ? true : false;

    this.categoryComponentRef.instance.loadCategoryList.subscribe(event => {
      this.destroyCategoryAndReload();
    });

    this.categoryComponentRef.instance.destroyComponent.subscribe(event => {
      this.destroyCategoryAndReload();
    });
  }

  addContact(categoryId: string) {
    this.contactContainer.clear();
    const factory = this.resolver.resolveComponentFactory(ContactComponent);
    this.contactComponentRef = this.contactContainer.createComponent(factory);
    this.contactComponentRef.instance.contact = new Contact(categoryId);
    this.contactComponentRef.instance.isEditMode = true;
    this.contactComponentRef.instance.isNewContact = true;

    this.contactComponentRef.instance.reloadContacts.subscribe(event => {
      this.getContacts(categoryId);
      this.contactComponentRef.destroy();
    });

    this.contactComponentRef.instance.destroyComponent.subscribe(event => {
      this.contactComponentRef.destroy();
    });
  }

  toggle(categoryId: string, e){
    if(e.target.checked) {
      this.getContacts(categoryId);
    }
  }

  destroyCategoryAndReload() {
    this.categoryComponentRef.destroy();
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
      .subscribe((result: any) => {
        category[0].contacts = result.data;
      },
      error => console.error(error));
    }
  }

  deleteCategory(category: Category){
    if(category) {
      this.categoryService.deleteCategory(category.id)
      .subscribe((result: any) => {
        this.loadCategoryList();
      },
      error => console.error(error));
    }
  }

}
