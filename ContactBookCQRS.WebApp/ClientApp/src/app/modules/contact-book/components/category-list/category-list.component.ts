import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver } from '@angular/core';
import { Category } from 'app/core/models/Category';
import { CategoryService } from '../../category.service';
import { faPlusCircle, faMinusCircle, faEdit, faUser, faHistory } from '@fortawesome/free-solid-svg-icons';
import { CategoryComponent } from '../category/category.component';
import { Contact } from 'app/core/models/Contact';
import { ContactComponent } from '../contact/contact.component';
import { ConfirmationDialogService } from 'app/core/services/confirmation-dialog.service';
import { HistoryViewerComponent } from '../history-viewer/history-viewer.component';
import { CategoryHistoryData } from 'app/core/models/CategoryHistoryData';
import { NotificationService } from 'app/core/services/notification.service';

@Component({
  selector: 'app-category-list',
  templateUrl: 'category-list.component.html',
  styleUrls: ['./category-list.scss']
})
export class CategoryListComponent implements OnInit {

  @ViewChild("categoryContainer", { read: ViewContainerRef }) categoryContainer;
  @ViewChild("contactContainer", { read: ViewContainerRef }) contactContainer;
  @ViewChild("historyViewerContainer", { read: ViewContainerRef }) historyViewerContainer;

  categories: Category[];
  categoryToEdit: Category;
  categoryComponentRef: any;
  contactComponentRef: any;
  historyViewerComponentRef: any;
  createEditCategory = false;
  modalOpened = false;

  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;
  faEdit = faEdit;
  faUser = faUser;
  faHistory = faHistory;

  constructor(
    private categoryService: CategoryService,
    private resolver: ComponentFactoryResolver,
    private confirmationDialogService: ConfirmationDialogService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.loadCategoryList();
  }

  addEditCategory(category?: Category) {
    this.modalOpened = true;
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
    this.modalOpened = true;
    this.contactContainer.clear();
    const factory = this.resolver.resolveComponentFactory(ContactComponent);
    this.contactComponentRef = this.contactContainer.createComponent(factory);
    this.contactComponentRef.instance.contact = new Contact(categoryId);
    this.contactComponentRef.instance.isEditMode = true;
    this.contactComponentRef.instance.isNewContact = true;

    this.contactComponentRef.instance.reloadContacts.subscribe(event => {
      this.getContacts(categoryId);
      this.contactComponentRef.destroy();
      this.modalOpened = false;
    });

    this.contactComponentRef.instance.destroyComponent.subscribe(event => {
      this.contactComponentRef.destroy();
      this.modalOpened = false;
    });
  }

  showHistoryViewer(categoryId: string) {
    this.modalOpened = true;
    this.historyViewerContainer.clear();
    const factory = this.resolver.resolveComponentFactory(HistoryViewerComponent);
    this.historyViewerComponentRef = this.historyViewerContainer.createComponent(factory);
    this.historyViewerComponentRef.instance.aggregateId = categoryId;
    this.historyViewerComponentRef.instance.aggregateType = "CategoryHistoryData";

    this.historyViewerComponentRef.instance.destroyComponent.subscribe(event => {
      this.historyViewerComponentRef.destroy();
      this.modalOpened = false;
    });
  }

  toggle(categoryId: string, e){
    if(e.target.checked) {
      this.getContacts(categoryId);
    }
  }

  destroyCategoryAndReload() {
    this.categoryComponentRef.destroy();
    this.modalOpened = false;
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
    var category = this.categories.
    filter((category : Category) => category.id === categoryId);

    if(category) {
      this.categoryService.getContacts(categoryId)
      .subscribe((result: any) => {
        category[0].contacts = result.data;
      },
      error => console.error(error));
    }
  }

  deleteCategory(category: Category) {
    this.confirmationDialogService.confirm('Please confirm', 'Do you really want to remove this category?')
    .then((confirmed) => {
      if(confirmed && category) {
        this.categoryService.deleteCategory(category.id)
        .subscribe((result: any) => {
          this.notificationService.showSuccess("Category successfully deleted!");
          this.loadCategoryList();
        },
        error => this.notificationService.showError(error));
      }
    })
    .catch();
  }
}
