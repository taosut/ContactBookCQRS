import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CategoryService } from '../../category.service';
import { Category } from 'app/core/models/Category';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  @Output("loadCategoryList") loadCategoryList: EventEmitter<any> = new EventEmitter();
  @Output("destroyComponent") destroyComponent: EventEmitter<any> = new EventEmitter();

  category: Category;
  isEditMode: boolean;
  categoryForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(private formBuilder: FormBuilder,
              private categoryService: CategoryService,
              private authService: AuthService) { }

  ngOnInit() {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
    });

    // new category
    if(!this.category) {
      this.category = new Category(
        this.authService.currentUserValue.contactBookId,'');
    }
  }

  // getter for easy access to form fields
  get f() {
    return this.categoryForm.controls;
  }

  cancel(){
    this.destroyComponent.emit();
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.categoryForm.invalid) {
        return;
    }

    //Create category
    if(!this.isEditMode) {
      this.createCategory();
    }
    else { //Update category
      this.updateCategory();
    }
  }

  createCategory() {
    this.loading = true;
    this.category.name = this.f.name.value;
    this.categoryService.createCategory(this.category)
    .subscribe(
      data => {
        this.loadCategoryList.emit();
      },
      error => {
          this.error = error;
          this.loading = false;
      });
  }

  updateCategory() {
    this.loading = true;
    this.categoryService.updateCategory(this.category)
    .subscribe(
      data => {
        this.loadCategoryList.emit();
      },
      error => {
          this.error = error;
          this.loading = false;
      });
  }
}
