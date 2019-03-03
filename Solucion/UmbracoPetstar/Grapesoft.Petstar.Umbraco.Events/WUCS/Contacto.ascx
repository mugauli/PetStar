<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contacto.ascx.cs" Inherits="Grapesoft.Petstar.Events.WUCS.Contacto" %>


<div class="row">
    <div class="form-group col-12 col-md-6 col-lg-4">
        <input type="text" class="form-control" id="txtName" placeholder="Nombre">
    </div>
    <div class="form-group col-12 col-md-6 col-lg-4">
        <input type="email" class="form-control" id="txtEmail" placeholder="Correo electrónico">
    </div>
    <div class="form-group col-12 col-md-6 col-lg-4">
        <input type="text" class="form-control" id="txtCiudad" placeholder="Ciudad">
    </div>
    <div class="form-group col-12 col-md-6 col-lg-4">
        <label class="col-6">Tipo de teléfono</label>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1">
            <label class="form-check-label" for="inlineRadio1">Fijo</label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
            <label class="form-check-label" for="inlineRadio2">Móvil</label>
        </div>
    </div>
    <div class="form-group col-12 col-md-6 col-lg-4">
        <input type="email" class="form-control" id="txtTTelefono" placeholder="Ingrese número">
    </div>
    <div class="form-group col-12 col-md-6 col-lg-4">
        <select class="form-control" id="sltAsunto">
            <option value="0">Selecciona un asunto</option>
            <option value="1">Me interesa vender PET </option>
            <option value="2">Quiero programar una visita guiada al Museo-Auditorio </option>
            <option value="3">Quiero ser proveedor de productos o servicios</option>
            <option value="4">Soy medio de comunicación y me interesa más información </option>
            <option value="5">Quiero recibir noticias de PetStar </option>
            <option value="6">Soy Fundación o Asociación </option>
            <!--<option value="7">Otro</option>-->
            <option value="8">Quiero trabajar en PetStar</option>
            <option value="9">Centro de atención al socio acopiador </option>
        </select>
    </div>
    <div class="form-group col-12">
        <textarea class="form-control" id="txtrMensaje" rows="3" placeholder="Mensaje"></textarea>
    </div>
    <div class="form-group col-12">
        <button class="btn btn-primary">ENVIAR</button>
        
    </div>

</div>
