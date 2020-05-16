import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CategoryService } from '../../category.service';
import { Category } from 'app/core/models/Category';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {

  @Output("loadCategoryList") loadCategoryList: EventEmitter<any> = new EventEmitter();
  @Output("cancelAddCategory") cancelAddCategory: EventEmitter<any> = new EventEmitter();

  categoryForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  constructor(private formBuilder: FormBuilder,
              private categoryService: CategoryService,
              private authService: AuthService) { }

  ngOnInit() {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  // getter for easy access to form fields
  get f() {
    return this.categoryForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.categoryForm.invalid) {
        return;
    }

    this.loading = true;
    let category = new Category(
      this.authService.currentUserValue.contactBookId,
      this.f.name.value);

    this.categoryService.addCategory(category)
    .subscribe(
      data => {
        this.loadCategoryList.emit();
        this.cancelAddCategory.emit();
      },
      error => {
          this.error = error;
          this.loading = false;
      });
  }
}
