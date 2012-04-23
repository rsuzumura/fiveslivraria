alter Proc spCadastra_usuario(  
 @nmUsuario varchar(200)  
,@dsEndereco varchar(200) = ''  
,@tpUsuario smallint = 0  
,@dsLogin varchar(100) = ''  
,@senha  varchar(100) = ''  
,@dsPergunta varchar(500) = ''  
,@dsResposta int = 0  
,@dsEmail varchar(100) = ''  
)  
AS  
BEGIN  
 insert  into Usuarios(  
    nmUsuario   
    ,dsEndereco  
    ,tpUsuario   
    ,dsLogin   
    ,senha    
    ,dsPergunta  
    ,dsResposta  
    ,dsEmail   
    )Values(  
    @nmUsuario   
    ,@dsEndereco  
    ,@tpUsuario   
    ,@dsLogin   
    ,@senha    
    ,@dsPergunta  
    ,@dsResposta  
    ,@dsEmail   
    )  
    
  select 'a'
END