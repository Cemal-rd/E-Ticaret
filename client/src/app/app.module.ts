import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from '@angular/material/table';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule}  from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';

import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { ProductDetailComponent } from './Components/product/product-detail/product-detail.component';
import { ProductComponent } from './Components/product/product.component';
import { LoadingScreenComponent } from './Components/Shared/loading-screen/loading-screen.component';
import { ConfirmationDialogComponent } from './Components/Shared/confirmation-dialog/confirmation-dialog.component';
import { MatTableExporterModule } from 'mat-table-exporter';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';




@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ProductDetailComponent,
    LoadingScreenComponent,
    ConfirmationDialogComponent,
    LoginComponent,
    SignupComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    HttpClientModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatGridListModule,
    MatProgressSpinnerModule,
    MatTableExporterModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
