import { Component, OnInit, Input } from '@angular/core';
import { Contact } from 'app/core/models/Contact';
import { faEdit, faMinusCircle } from '@fortawesome/free-solid-svg-icons';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  contactForm: FormGroup;
  @Input() contact: Contact;
  faMinusCircle = faMinusCircle;
  faEdit = faEdit;
  submitted = false;
  readOnly = true;
  isEditable = false;
  error = '';

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.contactForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      birthday: ['', Validators.required]
    });
  }

  setEditable(){
    this.isEditable = !this.isEditable;
  }

  // getter for easy access to form fields
  get f() {
    return this.contactForm.controls;
  }

  onSubmit() {
  }
}
