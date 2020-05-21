import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryService } from './category.service';
import { SharedModule } from 'app/shared/shared.module';
import { CategoryComponent } from './components/category/category.component';
import { ContactComponent } from './components/contact/contact.component';
import { ContactService } from './contact.service';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { HistoryViewerComponent } from './components/history-viewer/history-viewer.component';

@NgModule({
   declarations: [
      HomeComponent,
      CategoryListComponent,
      CategoryComponent,
      ContactComponent,
      HistoryViewerComponent
   ],
   imports: [
      CommonModule,
      SharedModule,
      NgbDatepickerModule
   ],
   providers: [
      CategoryService,
      ContactService
   ]
})
export class ContactBookModule { }
