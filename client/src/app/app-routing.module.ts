import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './Components/product/product-detail/product-detail.component';
import { ProductComponent } from './Components/product/product.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  {
    path: ' ', redirectTo: 'login'
  }, {
    path: 'login', component: LoginComponent
  },
  {
    path: 'signup', component: SignupComponent
  },

  {
    path: 'product',
    component: ProductComponent,
    pathMatch: 'full'
  },
  {
    path: 'product/:id',
    component: ProductDetailComponent,
    pathMatch: 'full'
  },
  {
    path: 'create-product',
    component: ProductDetailComponent,
    pathMatch: 'full'
  },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
