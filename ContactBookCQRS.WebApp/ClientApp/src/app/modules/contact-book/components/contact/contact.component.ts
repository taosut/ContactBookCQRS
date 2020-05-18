import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Contact } from 'app/core/models/Contact';
import { faEdit, faMinusCircle } from '@fortawesome/free-solid-svg-icons';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {

  @Output("reloadContacts") reloadContacts: EventEmitter<any> = new EventEmitter();
  @Output("destroyComponent") destroyComponent: EventEmitter<any> = new EventEmitter();
  @Input() contact: Contact;

  isEditMode: boolean;
  isNewContact: boolean;
  contactForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  faMinusCircle = faMinusCircle;
  faEdit = faEdit;

  constructor(private formBuilder: FormBuilder,
              private contactService: ContactService) { }

  ngOnInit() {
    this.contactForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      birthDate: ['', Validators.required]
    });
  }

  setEditable(){
    this.isEditMode = !this.isEditMode;
  }

  // getter for easy access to form fields
  get f() {
    return this.contactForm.controls;
  }

  cancel(){
    this.isEditMode = false;
    this.destroyComponent.emit();
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.contactForm.invalid) {
        return;
    }

    //Create contact
    if(this.isEditMode && this.isNewContact) {
      this.createContact();
    }
    else { //Update contact
      this.updateContact();
    }
  }

  createContact() {

    // stop here if form is invalid
    if (this.contactForm.invalid) {
        return;
    }

    this.loading = true;
    this.contact.name = this.f.name.value;
    this.contact.email = this.f.email.value;
    this.contact.birthDate = this.f.birthDate.value;

    this.contactService.createContact(this.contact)
    .subscribe(
      data => {
        this.reloadContacts.emit();
      },
      error => {
          this.error = error;
          this.loading = false;
      });
  }

  updateContact() {
    // stop here if form is invalid
    if (this.contactForm.invalid) {
        return;
    }
    this.loading = true;
    this.contactService.updateContact(this.contact)
    .subscribe(
      data => {
        this.cancel();
      },
      error => {
          this.error = error;
          this.loading = false;
      });
  }

  deleteContact() {
    this.loading = true;
    this.contactService.deleteContact(this.contact.id)
    .subscribe(
      data => {
        this.reloadContacts.emit(this.contact.categoryId);
      },
      error => {
          this.error = error;
          this.loading = false;
      });
  }

}
