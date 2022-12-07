å
ID:\Tempus\Tempus.Core\Commands\Categories\Create\CreateCategoryCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Create* 0
;0 1
public 
class !
CreateCategoryCommand "
:# $
IRequest% -
<- .
BaseResponse. :
<: ;
BaseCategory; G
>G H
>H I
{ 
public		 

Guid		 
UserId		 
{		 
get		 
;		 
init		 "
;		" #
}		$ %
public

 

string

 
Name

 
{

 
get

 
;

 
init

 "
;

" #
}

$ %
public 

string 
? 
Color 
{ 
get 
; 
init  $
;$ %
}& '
} î!
PD:\Tempus\Tempus.Core\Commands\Categories\Create\CreateCategoryCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Create* 0
;0 1
public		 
class		 (
CreateCategoryCommandHandler		 )
:		* +
IRequestHandler		, ;
<		; <!
CreateCategoryCommand		< Q
,		Q R
BaseResponse		S _
<		_ `
BaseCategory		` l
>		l m
>		m n
{

 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
private 
readonly 
IUserRepository $
_userRepository% 4
;4 5
public 
(
CreateCategoryCommandHandler '
(' (
ICategoryRepository( ;
categoryRepository< N
,N O
IUserRepositoryP _
userRepository` n
)n o
{ 
_categoryRepository 
= 
categoryRepository 0
;0 1
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseCategory# /
>/ 0
>0 1
Handle2 8
(8 9!
CreateCategoryCommand9 N
requestO V
,V W
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
user 
= 
await 
_userRepository ,
., -
GetById- 4
(4 5
request5 <
.< =
UserId= C
)C D
;D E
if 
( 
user 
== 
null 
) 
return $
BaseResponse% 1
<1 2
BaseCategory2 >
>> ?
.? @

BadRequest@ J
(J K
$"K M
$strM [
{[ \
request\ c
.c d
UserIdd j
}j k
$strk u
"u v
)v w
;w x
var 
entity 
= 
new 
Category %
(% &
Guid& *
.* +
NewGuid+ 2
(2 3
)3 4
,4 5
request6 =
.= >
Name> B
,B C
DateTimeD L
.L M
UtcNowM S
,S T
DateTimeU ]
.] ^
UtcNow^ d
,d e
requestf m
.m n
Colorn s
,s t
request 
. 
UserId 
) 
;  
var   
category   
=   
await    
_categoryRepository  ! 4
.  4 5
Add  5 8
(  8 9
entity  9 ?
)  ? @
;  @ A
var"" 
result"" 
="" 
BaseResponse## 
<## 
BaseCategory## )
>##) *
.##* +
Ok##+ -
(##- .
new##. 1
BaseCategory##2 >
(##> ?
category##? G
.##G H
Id##H J
,##J K
category##L T
.##T U
Name##U Y
,##Y Z
category##[ c
.##c d
	CreatedAt##d m
,##m n
category$$ 
.$$ 
Color$$ "
,$$" #
category$$$ ,
.$$, -
UserId$$- 3
)$$3 4
)$$4 5
;$$5 6
return%% 
result%% 
;%% 
}&& 	
catch'' 
('' 
	Exception'' 
	exception'' "
)''" #
{(( 	
return)) 
BaseResponse)) 
<))  
BaseCategory))  ,
>)), -
.))- .

BadRequest)). 8
())8 9
	exception))9 B
.))B C
Message))C J
)))J K
;))K L
}** 	
}++ 
},, —
ID:\Tempus\Tempus.Core\Commands\Categories\Delete\DeleteCategoryCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Delete* 0
;0 1
public 
class !
DeleteCategoryCommand "
:# $
IRequest% -
<- .
BaseResponse. :
<: ;
Guid; ?
>? @
>@ A
{ 
public 

Guid 
Id 
{ 
get 
; 
init 
; 
}  !
}		 á
PD:\Tempus\Tempus.Core\Commands\Categories\Delete\DeleteCategoryCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Delete* 0
;0 1
public 
class (
DeleteCategoryCommandHandler )
:* +
IRequestHandler, ;
<; <!
DeleteCategoryCommand< Q
,Q R
BaseResponseS _
<_ `
Guid` d
>d e
>e f
{ 
private		 
readonly		 
ICategoryRepository		 (
_categoryRepository		) <
;		< =
public 
(
DeleteCategoryCommandHandler '
(' (
ICategoryRepository( ;
categoryRepository< N
)N O
{ 
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" #
Guid# '
>' (
>( )
Handle* 0
(0 1!
DeleteCategoryCommand1 F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
deletedCategoryId !
=" #
await$ )
_categoryRepository* =
.= >
Delete> D
(D E
requestE L
.L M
IdM O
)O P
;P Q
var 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,
Ok, .
(. /
deletedCategoryId/ @
)@ A
;A B
return 
result 
; 
} 	
catch 
( 
	Exception 
	exception "
)" #
{ 	
var 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,

BadRequest, 6
(6 7
	exception7 @
.@ A
MessageA H
)H I
;I J
return 
result 
; 
} 	
}   
}!! á
ID:\Tempus\Tempus.Core\Commands\Categories\Update\UpdateCategoryCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Update* 0
;0 1
public 
class !
UpdateCategoryCommand "
:# $
IRequest% -
<- .
BaseResponse. :
<: ;
BaseCategory; G
>G H
>H I
{ 
public		 

Guid		 
Id		 
{		 
get		 
;		 
init		 
;		 
}		  !
public

 

string

 
Name

 
{

 
get

 
;

 
init

 "
;

" #
}

$ %
public 

string 
? 
Color 
{ 
get 
; 
init  $
;$ %
}& '
} … 
PD:\Tempus\Tempus.Core\Commands\Categories\Update\UpdateCategoryCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Update* 0
;0 1
public		 
class		 (
UpdateCategoryCommandHandler		 )
:		* +
IRequestHandler		, ;
<		; <!
UpdateCategoryCommand		< Q
,		Q R
BaseResponse		S _
<		_ `
BaseCategory		` l
>		l m
>		m n
{

 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
public 
(
UpdateCategoryCommandHandler '
(' (
ICategoryRepository( ;
categoryRepository< N
)N O
{ 
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseCategory# /
>/ 0
>0 1
Handle2 8
(8 9!
UpdateCategoryCommand9 N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
entity 
= 
await 
_categoryRepository 2
.2 3
GetById3 :
(: ;
request; B
.B C
IdC E
)E F
;F G
if 
( 
entity 
== 
null 
) 
return 
BaseResponse #
<# $
BaseCategory$ 0
>0 1
.1 2
NotFound2 :
(: ;
$" 
$str (
{( )
request) 0
.0 1
Id1 3
}3 4
$str4 ?
"? @
)@ A
;A B
entity 
= 
new 
Category !
(! "
entity" (
.( )
Id) +
,+ ,
request- 4
.4 5
Name5 9
,9 :
entity; A
.A B
	CreatedAtB K
,K L
DateTimeM U
.U V
UtcNowV \
,\ ]
request^ e
.e f
Colorf k
,k l
entity 
. 
UserId 
) 
; 
var   
category   
=   
await    
_categoryRepository  ! 4
.  4 5
Update  5 ;
(  ; <
entity  < B
)  B C
;  C D
var"" 
result"" 
="" 
BaseResponse"" %
<""% &
BaseCategory""& 2
>""2 3
.""3 4
Ok""4 6
(""6 7
new""7 :
BaseCategory""; G
(""G H
category""H P
.""P Q
Id""Q S
,""S T
category""U ]
.""] ^
Name""^ b
,""b c
category## 
.## 
LastUpdatedAt## &
,##& '
category$$ 
.$$ 
Color$$ 
,$$ 
category$$  (
.$$( )
UserId$$) /
)$$/ 0
)$$0 1
;$$1 2
return%% 
result%% 
;%% 
}&& 	
catch'' 
('' 
	Exception'' 
	exception'' "
)''" #
{(( 	
var)) 
result)) 
=)) 
BaseResponse)) %
<))% &
BaseCategory))& 2
>))2 3
.))3 4

BadRequest))4 >
())> ?
	exception))? H
.))H I
Message))I P
)))P Q
;))Q R
return** 
result** 
;** 
}++ 	
},, 
}-- ‘
PD:\Tempus\Tempus.Core\Commands\Registrations\Create\CreateRegistrationCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Create- 3
;3 4
public 
class %
CreateRegistrationCommand &
:' (
IRequest) 1
<1 2
BaseResponse2 >
<> ? 
DetailedRegistration? S
>S T
>T U
{ 
public		 

string		 
?		 
Title		 
{		 
get		 
;		 
init		  $
;		$ %
}		& '
public

 

string

 
?

 
Content

 
{

 
get

  
;

  !
init

" &
;

& '
}

( )
public 

Guid 

CategoryId 
{ 
get  
;  !
init" &
;& '
}( )
} À#
WD:\Tempus\Tempus.Core\Commands\Registrations\Create\CreateRegistrationCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Create- 3
;3 4
public		 
class		 ,
 CreateRegistrationCommandHandler		 -
:		. /
IRequestHandler		0 ?
<		? @%
CreateRegistrationCommand		@ Y
,		Y Z
BaseResponse		[ g
<		g h 
DetailedRegistration		h |
>		| }
>		} ~
{

 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
private 
readonly #
IRegistrationRepository ,#
_registrationRepository- D
;D E
public 
,
 CreateRegistrationCommandHandler +
(+ ,#
IRegistrationRepository, C"
registrationRepositoryD Z
,Z [
ICategoryRepository 
categoryRepository .
). /
{ #
_registrationRepository 
=  !"
registrationRepository" 8
;8 9
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" # 
DetailedRegistration# 7
>7 8
>8 9
Handle: @
(@ A%
CreateRegistrationCommandA Z
request[ b
,b c
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
category 
= 
await  
_categoryRepository! 4
.4 5
GetById5 <
(< =
request= D
.D E

CategoryIdE O
)O P
;P Q
if 
( 
category 
== 
null  
)  !
return 
BaseResponse #
<# $ 
DetailedRegistration$ 8
>8 9
.9 :

BadRequest: D
(D E
$"E G
$strG Y
{Y Z
requestZ a
.a b

CategoryIdb l
}l m
$strm w
"w x
)x y
;y z
var   
entity   
=   
new   
Registration   )
(  ) *
Guid  * .
.  . /
NewGuid  / 6
(  6 7
)  7 8
,  8 9
request  : A
.  A B
Title  B G
,  G H
request  I P
.  P Q
Content  Q X
,  X Y
DateTime  Z b
.  b c
UtcNow  c i
,  i j
DateTime  k s
.  s t
UtcNow  t z
,  z {
category!! 
.!! 
Id!! 
)!! 
;!! 
var## 
registration## 
=## 
await## $#
_registrationRepository##% <
.##< =
Add##= @
(##@ A
entity##A G
)##G H
;##H I
var%% 
result%% 
=%% 
BaseResponse%% %
<%%% & 
DetailedRegistration%%& :
>%%: ;
.%%; <
Ok%%< >
(%%> ?
new%%? B 
DetailedRegistration%%C W
{&& 
Id'' 
='' 
registration'' !
.''! "
Id''" $
,''$ %
Title(( 
=(( 
registration(( $
.(($ %
Title((% *
,((* +
Content)) 
=)) 
registration)) &
.))& '
Content))' .
}** 
)** 
;** 
return++ 
result++ 
;++ 
},, 	
catch-- 
(-- 
	Exception-- 
	exception-- "
)--" #
{.. 	
var// 
result// 
=// 
BaseResponse// %
<//% & 
DetailedRegistration//& :
>//: ;
.//; <

BadRequest//< F
(//F G
	exception//G P
.//P Q
Message//Q X
)//X Y
;//Y Z
return00 
result00 
;00 
}11 	
}22 
}33 ¥
PD:\Tempus\Tempus.Core\Commands\Registrations\Delete\DeleteRegistrationCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Delete- 3
;3 4
public 
class %
DeleteRegistrationCommand &
:' (
IRequest) 1
<1 2
BaseResponse2 >
<> ?
Guid? C
>C D
>D E
{ 
public 

Guid 
Id 
{ 
get 
; 
init 
; 
}  !
}		 È
WD:\Tempus\Tempus.Core\Commands\Registrations\Delete\DeleteRegistrationCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Delete- 3
;3 4
public 
class ,
 DeleteRegistrationCommandHandler -
:. /
IRequestHandler0 ?
<? @%
DeleteRegistrationCommand@ Y
,Y Z
BaseResponse[ g
<g h
Guidh l
>l m
>m n
{ 
private		 
readonly		 #
IRegistrationRepository		 ,#
_registrationRepository		- D
;		D E
public 
,
 DeleteRegistrationCommandHandler +
(+ ,#
IRegistrationRepository, C"
registrationRepositoryD Z
)Z [
{ #
_registrationRepository 
=  !"
registrationRepository" 8
;8 9
} 
public 

async 
Task 
< 
BaseResponse "
<" #
Guid# '
>' (
>( )
Handle* 0
(0 1%
DeleteRegistrationCommand1 J
requestK R
,R S
CancellationTokenT e
cancellationTokenf w
)w x
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var !
deletedRegistrationId %
=& '
await( -#
_registrationRepository. E
.E F
DeleteF L
(L M
requestM T
.T U
IdU W
)W X
;X Y
var 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,
Ok, .
(. /!
deletedRegistrationId/ D
)D E
;E F
return 
result 
; 
} 	
catch 
( 
	Exception 
	exception "
)" #
{ 	
return 
BaseResponse 
<  
Guid  $
>$ %
.% &

BadRequest& 0
(0 1
	exception1 :
.: ;
Message; B
)B C
;C D
} 	
} 
}   ‰
PD:\Tempus\Tempus.Core\Commands\Registrations\Update\UpdateRegistrationCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Update- 3
;3 4
public 
class %
UpdateRegistrationCommand &
:' (
IRequest) 1
<1 2
BaseResponse2 >
<> ? 
DetailedRegistration? S
>S T
>T U
{ 
public		 

Guid		 
Id		 
{		 
get		 
;		 
init		 
;		 
}		  !
public

 

string

 
?

 
Title

 
{

 
get

 
;

 
init

  $
;

$ %
}

& '
public 

string 
? 
Content 
{ 
get  
;  !
init" &
;& '
}( )
} Ý 
WD:\Tempus\Tempus.Core\Commands\Registrations\Update\UpdateRegistrationCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Update- 3
;3 4
public		 
class		 ,
 UpdateRegistrationCommandHandler		 -
:		. /
IRequestHandler		0 ?
<		? @%
UpdateRegistrationCommand		@ Y
,		Y Z
BaseResponse		[ g
<		g h 
DetailedRegistration		h |
>		| }
>		} ~
{

 
private 
readonly #
IRegistrationRepository ,#
_registrationRepository- D
;D E
public 
,
 UpdateRegistrationCommandHandler +
(+ ,#
IRegistrationRepository, C"
registrationRepositoryD Z
)Z [
{ #
_registrationRepository 
=  !"
registrationRepository" 8
;8 9
} 
public 

async 
Task 
< 
BaseResponse "
<" # 
DetailedRegistration# 7
>7 8
>8 9
Handle: @
(@ A%
UpdateRegistrationCommandA Z
request[ b
,b c
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
entity 
= 
await #
_registrationRepository 6
.6 7
GetById7 >
(> ?
request? F
.F G
IdG I
)I J
;J K
if 
( 
entity 
== 
null 
) 
return 
BaseResponse #
<# $ 
DetailedRegistration$ 8
>8 9
.9 :

BadRequest: D
(D E
$"E G
$strG ]
{] ^
request^ e
.e f
Idf h
}h i
$stri w
"w x
)x y
;y z
entity 
= 
new 
Registration %
(% &
entity& ,
., -
Id- /
,/ 0
request1 8
.8 9
Title9 >
,> ?
request@ G
.G H
ContentH O
,O P
entityQ W
.W X
	CreatedAtX a
,a b
DateTimec k
.k l
UtcNowl r
,r s
entity 
. 

CategoryId !
)! "
;" #
var!! 
registration!! 
=!! 
await!! $#
_registrationRepository!!% <
.!!< =
Update!!= C
(!!C D
entity!!D J
)!!J K
;!!K L
var## 
result## 
=## 
BaseResponse## %
<##% & 
DetailedRegistration##& :
>##: ;
.##; <
Ok##< >
(##> ?
new##? B 
DetailedRegistration##C W
{$$ 
Id%% 
=%% 
registration%% !
.%%! "
Id%%" $
,%%$ %
Title&& 
=&& 
registration&& $
.&&$ %
Title&&% *
,&&* +
Content'' 
='' 
registration'' &
.''& '
Content''' .
}(( 
)(( 
;(( 
return)) 
result)) 
;)) 
}** 	
catch++ 
(++ 
	Exception++ 
	exception++ "
)++" #
{,, 	
var-- 
result-- 
=-- 
BaseResponse-- %
<--% & 
DetailedRegistration--& :
>--: ;
.--; <

BadRequest--< F
(--F G
	exception--G P
.--P Q
Message--Q X
)--X Y
;--Y Z
return.. 
result.. 
;.. 
}// 	
}00 
}11 «
@D:\Tempus\Tempus.Core\Commands\Users\Create\CreateUserCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Create% +
;+ ,
public 
class 
CreateUserCommand 
:  
IRequest! )
<) *
BaseResponse* 6
<6 7
BaseUser7 ?
>? @
>@ A
{ 
public		 

string		 
UserName		 
{		 
get		  
;		  !
init		" &
;		& '
}		( )
public

 

string

 
Email

 
{

 
get

 
;

 
init

 #
;

# $
}

% &
} Î
GD:\Tempus\Tempus.Core\Commands\Users\Create\CreateUserCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Create% +
;+ ,
public		 
class		 $
CreateUserCommandHandler		 %
:		& '
IRequestHandler		( 7
<		7 8
CreateUserCommand		8 I
,		I J
BaseResponse		K W
<		W X
BaseUser		X `
>		` a
>		a b
{

 
private 
readonly 
IUserRepository $
_userRepository% 4
;4 5
public 
$
CreateUserCommandHandler #
(# $
IUserRepository$ 3
userRepository4 B
)B C
{ 
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseUser# +
>+ ,
>, -
Handle. 4
(4 5
CreateUserCommand5 F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
entity 
= 
new 
User !
(! "
Guid" &
.& '
NewGuid' .
(. /
)/ 0
,0 1
request2 9
.9 :
UserName: B
,B C
requestD K
.K L
EmailL Q
)Q R
;R S
var 
user 
= 
await 
_userRepository ,
., -
Add- 0
(0 1
entity1 7
)7 8
;8 9
var 
result 
= 
BaseResponse %
<% &
BaseUser& .
>. /
./ 0
Ok0 2
(2 3
new3 6
BaseUser7 ?
( 
user 
. 
Id 
, 
user 
. 
UserName 
, 
user 
. 
Email 
)   
)   
;   
return"" 
result"" 
;"" 
}## 	
catch$$ 
($$ 
	Exception$$ 
	exception$$ "
)$$" #
{%% 	
var&& 
result&& 
=&& 
BaseResponse&& %
<&&% &
BaseUser&&& .
>&&. /
.&&/ 0

BadRequest&&0 :
(&&: ;
	exception&&; D
.&&D E
Message&&E L
)&&L M
;&&M N
return'' 
result'' 
;'' 
}(( 	
})) 
}** …
@D:\Tempus\Tempus.Core\Commands\Users\Delete\DeleteUserCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Delete% +
;+ ,
public 
class 
DeleteUserCommand 
:  
IRequest! )
<) *
BaseResponse* 6
<6 7
Guid7 ;
>; <
>< =
{ 
public 

Guid 
Id 
{ 
get 
; 
init 
; 
}  !
}		 Ÿ
GD:\Tempus\Tempus.Core\Commands\Users\Delete\DeleteUserCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Delete% +
;+ ,
public 
class $
DeleteUserCommandHandler %
:& '
IRequestHandler( 7
<7 8
DeleteUserCommand8 I
,I J
BaseResponseK W
<W X
GuidX \
>\ ]
>] ^
{ 
private		 
readonly		 
IUserRepository		 $
_userRepository		% 4
;		4 5
public 
$
DeleteUserCommandHandler #
(# $
IUserRepository$ 3
userRepository4 B
)B C
{ 
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
Guid# '
>' (
>( )
Handle* 0
(0 1
DeleteUserCommand1 B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
deletedUserId 
= 
await  %
_userRepository& 5
.5 6
Delete6 <
(< =
request= D
.D E
IdE G
)G H
;H I
var 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,
Ok, .
(. /
deletedUserId/ <
)< =
;= >
return 
result 
; 
} 	
catch 
( 
	Exception 
	exception "
)" #
{ 	
var 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,

BadRequest, 6
(6 7
	exception7 @
.@ A
MessageA H
)H I
;I J
return 
result 
; 
} 	
} 
}   À
@D:\Tempus\Tempus.Core\Commands\Users\Update\UpdateUserCommand.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Update% +
;+ ,
public 
class 
UpdateUserCommand 
:  
IRequest! )
<) *
BaseResponse* 6
<6 7
BaseUser7 ?
>? @
>@ A
{ 
public		 

Guid		 
Id		 
{		 
get		 
;		 
init		 
;		 
}		  !
public

 

string

 
UserName

 
{

 
get

  
;

  !
init

" &
;

& '
}

( )
public 

string 
Email 
{ 
get 
; 
init #
;# $
}% &
} ¤
GD:\Tempus\Tempus.Core\Commands\Users\Update\UpdateUserCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Update% +
;+ ,
public		 
class		 $
UpdateUserCommandHandler		 %
:		& '
IRequestHandler		( 7
<		7 8
UpdateUserCommand		8 I
,		I J
BaseResponse		K W
<		W X
BaseUser		X `
>		` a
>		a b
{

 
private 
readonly 
IUserRepository $
_userRepository% 4
;4 5
public 
$
UpdateUserCommandHandler #
(# $
IUserRepository$ 3
userRepository4 B
)B C
{ 
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseUser# +
>+ ,
>, -
Handle. 4
(4 5
UpdateUserCommand5 F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
user 
= 
await 
_userRepository ,
., -
GetById- 4
(4 5
request5 <
.< =
Id= ?
)? @
;@ A
if 
( 
user 
== 
null 
) 
return $
BaseResponse% 1
<1 2
BaseUser2 :
>: ;
.; <

BadRequest< F
(F G
$"G I
$strI V
{V W
requestW ^
.^ _
Id_ a
}a b
$strb l
"l m
)m n
;n o
user 
= 
new 
User 
( 
user  
.  !
Id! #
,# $
request% ,
., -
UserName- 5
,5 6
request7 >
.> ?
Email? D
)D E
;E F
await 
_userRepository !
.! "
Update" (
(( )
user) -
)- .
;. /
var   
result   
=   
BaseResponse   %
<  % &
BaseUser  & .
>  . /
.  / 0
Ok  0 2
(  2 3
new  3 6
BaseUser  7 ?
(  ? @
user  @ D
.  D E
Id  E G
,  G H
user  I M
.  M N
UserName  N V
,  V W
user  X \
.  \ ]
Email  ] b
)  b c
)  c d
;  d e
return!! 
result!! 
;!! 
}"" 	
catch## 
(## 
	Exception## 
	exception## "
)##" #
{$$ 	
return%% 
BaseResponse%% 
<%%  
BaseUser%%  (
>%%( )
.%%) *

BadRequest%%* 4
(%%4 5
	exception%%5 >
.%%> ?
Message%%? F
)%%F G
;%%G H
}&& 	
}'' 
}(( Å
-D:\Tempus\Tempus.Core\Commons\BaseResponse.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commons 
; 
public 
class 
BaseResponse 
< 
T 
> 
{ 
public 

T 
? 
Resource 
{ 
get 
; 
set !
;! "
}# $
[		 

JsonIgnore		 
]		 
public		 
StatusCodes		 #

StatusCode		$ .
{		/ 0
get		1 4
;		4 5
set		6 9
;		9 :
}		; <
public 

List 
< 
string 
> 
? 
Errors 
{  !
get" %
;% &
set' *
;* +
}, -
public 

static 
BaseResponse 
< 
T  
>  !
Ok" $
($ %
T% &
resource' /
)/ 0
{ 
return 
new 
BaseResponse 
<  
T  !
>! "
{ 	
Resource 
= 
resource 
,  

StatusCode 
= 
StatusCodes $
.$ %
Ok% '
} 	
;	 

} 
public 

static 
BaseResponse 
< 
T  
>  !
NotFound" *
(* +
string+ 1
resource2 :
): ;
{ 
return 
new 
BaseResponse 
<  
T  !
>! "
{ 	

StatusCode 
= 
StatusCodes $
.$ %
NotFound% -
,- .
Errors 
= 
new 
List 
< 
string $
>$ %
{ 
$" 
{ 
resource 
} 
$str &
"& '
} 
} 	
;	 

}   
public"" 

static"" 
BaseResponse"" 
<"" 
T""  
>""  !

BadRequest""" ,
("", -
string""- 3
message""4 ;
)""; <
{## 
return$$ 
new$$ 
BaseResponse$$ 
<$$  
T$$  !
>$$! "
{%% 	

StatusCode&& 
=&& 
StatusCodes&& $
.&&$ %

BadRequest&&% /
,&&/ 0
Errors'' 
='' 
new'' 
List'' 
<'' 
string'' $
>''$ %
{(( 
message)) 
}** 
}++ 	
;++	 

},, 
}-- Þ
,D:\Tempus\Tempus.Core\Commons\StatusCodes.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commons 
; 
public 
enum 
StatusCodes 
{ 
Ok 
, 

BadRequest 
, 
NotFound 
} §
,D:\Tempus\Tempus.Core\Entities\BaseEntity.cs
	namespace 	
Tempus
 
. 
Core 
. 
Entities 
; 
public 
class 

BaseEntity 
{ 
public 


BaseEntity 
( 
) 
{ 
} 
public		 


BaseEntity		 
(		 
Guid		 
id		 
)		 
{

 
Id 

= 
id 
; 
} 
public 

Guid 
Id 
{ 
get 
; 
} 
} ý
*D:\Tempus\Tempus.Core\Entities\Category.cs
	namespace 	
Tempus
 
. 
Core 
. 
Entities 
; 
public 
class 
Category 
: 

BaseEntity "
{ 
public 

Category 
( 
) 
{ 
} 
public

 

Category

 
(

 
Guid 
id 
, 
string 
name 
, 
DateTime 
	createdAt 
, 
DateTime 
lastUpdatedAt 
, 
string 
color 
, 
Guid 
userId 
, 
List 
< 
Registration 
> 
? 
registrations )
=* +
null, 0
,0 1
User 
? 
user 
= 
null 
) 
: 
base 
( 
id 
) 
{ 
Name 
= 
name 
; 
	CreatedAt 
= 
	createdAt 
; 
LastUpdatedAt 
= 
lastUpdatedAt %
;% &
Color 
= 
color 
; 
User 
= 
user 
; 
UserId 
= 
userId 
; 
Registrations 
= 
registrations %
;% &
} 
public 

string 
Name 
{ 
get 
; 
} 
=  !
$str" $
;$ %
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
}% &
public   

DateTime   
LastUpdatedAt   !
{  " #
get  $ '
;  ' (
}  ) *
public!! 

string!! 
?!! 
Color!! 
{!! 
get!! 
;!! 
}!!  !
public"" 

List"" 
<"" 
Registration"" 
>"" 
?"" 
Registrations"" ,
{""- .
get""/ 2
;""2 3
}""4 5
public## 

User## 
?## 
User## 
{## 
get## 
;## 
}## 
public$$ 

Guid$$ 
UserId$$ 
{$$ 
get$$ 
;$$ 
}$$ 
}%% â
.D:\Tempus\Tempus.Core\Entities\Registration.cs
	namespace 	
Tempus
 
. 
Core 
. 
Entities 
; 
public 
class 
Registration 
: 

BaseEntity &
{ 
public 

Registration 
( 
) 
{ 
} 
public		 

Registration		 
(		 
Guid

 
id

 
,

 
string 
? 
title 
, 
string 
? 
content 
, 
DateTime 
	createdAt 
, 
DateTime 
lastUpdatedAt 
, 
Guid 

categoryId 
, 
Category 
category 
= 
null  
)  !
:" #
base$ (
(( )
id) +
)+ ,
{ 
Title 
= 
title 
; 
Content 
= 
content 
; 
	CreatedAt 
= 
	createdAt 
; 
LastUpdatedAt 
= 
lastUpdatedAt %
;% &

CategoryId 
= 

categoryId 
;  
Category 
= 
category 
; 
} 
public 

string 
? 
Title 
{ 
get 
; 
}  !
public 

string 
? 
Content 
{ 
get  
;  !
}" #
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
}% &
public 

DateTime 
LastUpdatedAt !
{" #
get$ '
;' (
}) *
public 

Guid 

CategoryId 
{ 
get  
;  !
}" #
public 

Category 
Category 
{ 
get "
;" #
}$ %
}   Â
&D:\Tempus\Tempus.Core\Entities\User.cs
	namespace 	
Tempus
 
. 
Core 
. 
Entities 
; 
public 
class 
User 
: 

BaseEntity 
{ 
public 

User 
( 
) 
{ 
} 
public		 

User		 
(		 
Guid

 
id

 
,

 
string 
userName 
, 
string 
email 
, 
List 
< 
Registration 
> 
? 
registrations )
=* +
null, 0
)0 1
:2 3
base4 8
(8 9
id9 ;
); <
{ 
UserName 
= 
userName 
; 
Email 
= 
email 
; 
Registrations 
= 
registrations %
;% &
} 
public 

string 
UserName 
{ 
get  
;  !
}" #
public 

string 
Email 
{ 
get 
; 
}  
public 

List 
< 
Registration 
> 
? 
Registrations ,
{- .
get/ 2
;2 3
}4 5
public 

List 
< 
Category 
> 
? 

Categories %
{& '
get( +
;+ ,
}- .
} Î
5D:\Tempus\Tempus.Core\Models\Category\BaseCategory.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Category %
;% &
public 
class 
BaseCategory 
: 

BaseEntity &
{ 
public 

BaseCategory 
( 
Guid 
id 
, 
string		 
name		 
,		 
DateTime

 
lastUpdatedAt

 
,

 
string 
color 
, 
Guid 
userId 
) 
: 
base 
( 
id 
) 
{ 
Name 
= 
name 
; 
LastUpdatedAt 
= 
lastUpdatedAt %
;% &
Color 
= 
color 
; 
UserId 
= 
userId 
; 
} 
public 

string 
Name 
{ 
get 
; 
} 
public 

DateTime 
LastUpdatedAt !
{" #
get$ '
;' (
}) *
public 

string 
? 
Color 
{ 
get 
; 
}  !
public 

Guid 
UserId 
{ 
get 
; 
set !
;! "
}# $
} î
=D:\Tempus\Tempus.Core\Models\Registration\BaseRegistration.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Registration )
;) *
public 
class 
BaseRegistration 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
}  
AD:\Tempus\Tempus.Core\Models\Registration\DetailedRegistration.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Registration )
;) *
public 
class  
DetailedRegistration !
:" #
BaseRegistration$ 4
{ 
public 

string 
? 
Content 
{ 
get  
;  !
set" %
;% &
}' (
} ²
=D:\Tempus\Tempus.Core\Models\Registration\RegistrationInfo.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Registration )
;) *
public 
class 
RegistrationInfo 
: 
BaseRegistration  0
{ 
public 

DateTime 
LastUpdatedAt !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

string 
CategoryColor 
{  !
get" %
;% &
set' *
;* +
}, -
} ‘
-D:\Tempus\Tempus.Core\Models\User\BaseUser.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
User !
;! "
public 
class 
BaseUser 
: 

BaseEntity "
{ 
public 

BaseUser 
( 
Guid 
id 
, 
string #
userName$ ,
,, -
string. 4
email5 :
): ;
:< =
base> B
(B C
idC E
)E F
{ 
UserName		 
=		 
userName		 
;		 
Email

 
=

 
email

 
;

 
} 
public 

string 
UserName 
{ 
get  
;  !
}" #
public 

string 
Email 
{ 
get 
; 
}  
} ²
HD:\Tempus\Tempus.Core\Queries\Categories\GetAll\GetAllCategoriesQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 

Categories (
.( )
GetAll) /
;/ 0
public 
class !
GetAllCategoriesQuery "
:# $
IRequest% -
<- .
BaseResponse. :
<: ;
List; ?
<? @
BaseCategory@ L
>L M
>M N
>N O
{ 
public		 

Guid		 
?		 
UserId		 
=>		 
null		 
;		  
}

 £
OD:\Tempus\Tempus.Core\Queries\Categories\GetAll\GetAllCategoriesQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 

Categories (
.( )
GetAll) /
;/ 0
public		 
class		 (
GetAllCategoriesQueryHandler		 )
:		* +
IRequestHandler		, ;
<		; <!
GetAllCategoriesQuery		< Q
,		Q R
BaseResponse		S _
<		_ `
List		` d
<		d e
BaseCategory		e q
>		q r
>		r s
>		s t
{

 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
public 
(
GetAllCategoriesQueryHandler '
(' (
ICategoryRepository( ;
categoryRepository< N
)N O
{ 
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" #
List# '
<' (
BaseCategory( 4
>4 5
>5 6
>6 7
Handle8 >
(> ?!
GetAllCategoriesQuery? T
requestU \
,\ ]
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
List 
< 
Category 
> 

categories %
;% &
if 
( 
request 
. 
UserId 
. 
HasValue '
)' (

categories 
= 
await "
_categoryRepository# 6
.6 7
GetAll7 =
(= >
request> E
.E F
UserIdF L
.L M
ValueM R
)R S
;S T
else 

categories 
= 
await "
_categoryRepository# 6
.6 7
GetAll7 =
(= >
)> ?
;? @
var   
response   
=   
BaseResponse!! 
<!! 
List!! !
<!!! "
BaseCategory!!" .
>!!. /
>!!/ 0
.!!0 1
Ok!!1 3
(!!3 4

categories!!4 >
."" 
Select"" 
("" 
x"" 
=>""  
new""! $
BaseCategory""% 1
(""1 2
x""2 3
.""3 4
Id""4 6
,""6 7
x""8 9
.""9 :
Name"": >
,""> ?
x""@ A
.""A B
LastUpdatedAt""B O
,""O P
x""Q R
.""R S
Color""S X
,""X Y
x""Z [
.""[ \
UserId""\ b
)""b c
)""c d
.""d e
ToList""e k
(""k l
)""l m
)""m n
;""n o
return## 
response## 
;## 
}$$ 	
catch%% 
(%% 
	Exception%% 
	exception%% "
)%%" #
{&& 	
var'' 
response'' 
='' 
BaseResponse'' '
<''' (
List''( ,
<'', -
BaseCategory''- 9
>''9 :
>'': ;
.''; <

BadRequest''< F
(''F G
	exception''G P
.''P Q
Message''Q X
)''X Y
;''Y Z
return(( 
response(( 
;(( 
})) 	
}** 
}++ 
HD:\Tempus\Tempus.Core\Queries\Categories\GetById\GetCategoryByIdQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 

Categories (
.( )
GetById) 0
;0 1
public 
class  
GetCategoryByIdQuery !
:" #
IRequest$ ,
<, -
BaseResponse- 9
<9 :
BaseCategory: F
>F G
>G H
{ 
public		 

Guid		 
Id		 
{		 
get		 
;		 
init		 
;		 
}		  !
}

 Ô
OD:\Tempus\Tempus.Core\Queries\Categories\GetById\GetCategoryByIdQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 

Categories (
.( )
GetById) 0
;0 1
public 
class '
GetCategoryByIdQueryHandler (
:) *
IRequestHandler+ :
<: ; 
GetCategoryByIdQuery; O
,O P
BaseResponseQ ]
<] ^
BaseCategory^ j
>j k
>k l
{		 
private

 
readonly

 
ICategoryRepository

 (
_categoryRepository

) <
;

< =
public 
'
GetCategoryByIdQueryHandler &
(& '
ICategoryRepository' :
categoryRepository; M
)M N
{ 
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseCategory# /
>/ 0
>0 1
Handle2 8
(8 9 
GetCategoryByIdQuery9 M
requestN U
,U V
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
category 
= 
await  
_categoryRepository! 4
.4 5
GetById5 <
(< =
request= D
.D E
IdE G
)G H
;H I
if 
( 
category 
== 
null  
)  !
return 
BaseResponse #
<# $
BaseCategory$ 0
>0 1
.1 2
NotFound2 :
(: ;
$str )
)) *
;* +
var 
response 
= 
BaseResponse 
< 
BaseCategory )
>) *
.* +
Ok+ -
(- .
new. 1
BaseCategory2 >
(> ?
category? G
.G H
IdH J
,J K
categoryL T
.T U
NameU Y
,Y Z
category[ c
.c d
LastUpdatedAtd q
,q r
category   
.   
Color   "
,  " #
category  $ ,
.  , -
UserId  - 3
)  3 4
)  4 5
;  5 6
return!! 
response!! 
;!! 
}"" 	
catch## 
(## 
	Exception## 
	exception## "
)##" #
{$$ 	
return%% 
BaseResponse%% 
<%%  
BaseCategory%%  ,
>%%, -
.%%- .

BadRequest%%. 8
(%%8 9
	exception%%9 B
.%%B C
Message%%C J
)%%J K
;%%K L
}&& 	
}'' 
}(( Æ
ND:\Tempus\Tempus.Core\Queries\Registrations\GetAll\GetAllRegistrationsQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Registrations +
.+ ,
GetAll, 2
;2 3
public 
class $
GetAllRegistrationsQuery %
:& '
IRequest( 0
<0 1
BaseResponse1 =
<= >
List> B
<B C
RegistrationInfoC S
>S T
>T U
>U V
{ 
public		 

Guid		 
?		 

CategoryId		 
=>		 
null		 #
;		# $
}

 ›#
UD:\Tempus\Tempus.Core\Queries\Registrations\GetAll\GetAllRegistrationsQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Registrations +
.+ ,
GetAll, 2
;2 3
public		 
class		 +
GetAllRegistrationsQueryHandler		 ,
:		- .
IRequestHandler		/ >
<		> ?$
GetAllRegistrationsQuery		? W
,		W X
BaseResponse		Y e
<		e f
List		f j
<		j k
RegistrationInfo		k {
>		{ |
>		| }
>		} ~
{

 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
private 
readonly #
IRegistrationRepository ,#
_registrationRepository- D
;D E
public 
+
GetAllRegistrationsQueryHandler *
(* +#
IRegistrationRepository+ B"
registrationRepositoryC Y
,Y Z
ICategoryRepository 
categoryRepository .
). /
{ #
_registrationRepository 
=  !"
registrationRepository" 8
;8 9
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" #
List# '
<' (
RegistrationInfo( 8
>8 9
>9 :
>: ;
Handle< B
(B C$
GetAllRegistrationsQueryC [
request\ c
,c d
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
List 
< 
Registration 
> 
registrations ,
;, -
if 
( 
request 
. 

CategoryId "
." #
HasValue# +
)+ ,
registrations 
= 
await  %#
_registrationRepository& =
.= >
GetAll> D
(D E
requestE L
.L M

CategoryIdM W
.W X
ValueX ]
)] ^
;^ _
else 
registrations   
=   
await    %#
_registrationRepository  & =
.  = >
GetAll  > D
(  D E
)  E F
;  F G
var"" 
response"" 
="" 
BaseResponse"" '
<""' (
List""( ,
<"", -
RegistrationInfo""- =
>""= >
>""> ?
.""? @
Ok""@ B
(""B C
registrations""C P
.## 
Select## 
(## 
x## 
=>## 
{$$ 
var%% 
categoryColor%% %
=%%& '
_categoryRepository%%( ;
.%%; <
GetCategoryColor%%< L
(%%L M
x%%M N
.%%N O

CategoryId%%O Y
)%%Y Z
;%%Z [
return'' 
new'' 
RegistrationInfo'' /
{(( 
Id)) 
=)) 
x)) 
.)) 
Id)) !
,))! "
Title** 
=** 
x**  !
.**! "
Title**" '
,**' (
LastUpdatedAt++ %
=++& '
x++( )
.++) *
LastUpdatedAt++* 7
,++7 8
CategoryColor,, %
=,,& '
categoryColor,,( 5
}-- 
;-- 
}.. 
).. 
.// 
ToList// 
(// 
)// 
)// 
;// 
return00 
response00 
;00 
}11 	
catch22 
(22 
	Exception22 
	exception22 "
)22" #
{33 	
var44 
response44 
=44 
BaseResponse44 '
<44' (
List44( ,
<44, -
RegistrationInfo44- =
>44= >
>44> ?
.44? @

BadRequest44@ J
(44J K
	exception44K T
.44T U
Message44U \
)44\ ]
;44] ^
return55 
response55 
;55 
}66 	
}77 
}88 ³
OD:\Tempus\Tempus.Core\Queries\Registrations\GetById\GetRegistrationByIdQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Registrations +
.+ ,
GetById, 3
;3 4
public 
class $
GetRegistrationByIdQuery %
:& '
IRequest( 0
<0 1
BaseResponse1 =
<= > 
DetailedRegistration> R
>R S
>S T
{ 
public		 

Guid		 
Id		 
{		 
get		 
;		 
init		 
;		 
}		  !
}

 ú
VD:\Tempus\Tempus.Core\Queries\Registrations\GetById\GetRegistrationByIdQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Registrations +
.+ ,
GetById, 3
;3 4
public 
class +
GetRegistrationByIdQueryHandler ,
:- .
IRequestHandler/ >
<> ?$
GetRegistrationByIdQuery? W
,W X
BaseResponseY e
<e f 
DetailedRegistrationf z
>z {
>{ |
{		 
private

 
readonly

 #
IRegistrationRepository

 ,#
_registrationRepository

- D
;

D E
public 
+
GetRegistrationByIdQueryHandler *
(* +#
IRegistrationRepository+ B"
registrationRepositoryC Y
)Y Z
{ #
_registrationRepository 
=  !"
registrationRepository" 8
;8 9
} 
public 

async 
Task 
< 
BaseResponse "
<" # 
DetailedRegistration# 7
>7 8
>8 9
Handle: @
(@ A$
GetRegistrationByIdQueryA Y
requestZ a
,a b
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
registration 
= 
await $#
_registrationRepository% <
.< =
GetById= D
(D E
requestE L
.L M
IdM O
)O P
;P Q
if 
( 
registration 
== 
null  $
)$ %
return& ,
BaseResponse- 9
<9 : 
DetailedRegistration: N
>N O
.O P
NotFoundP X
(X Y
$strY r
)r s
;s t
var 
response 
= 
BaseResponse '
<' ( 
DetailedRegistration( <
>< =
.= >
Ok> @
(@ A
newA D 
DetailedRegistrationE Y
{ 
Id 
= 
registration !
.! "
Id" $
,$ %
Title   
=   
registration   $
.  $ %
Title  % *
,  * +
Content!! 
=!! 
registration!! &
.!!& '
Content!!' .
}"" 
)"" 
;"" 
return## 
response## 
;## 
}$$ 	
catch%% 
(%% 
	Exception%% 
	exception%% "
)%%" #
{&& 	
var'' 
response'' 
='' 
BaseResponse'' '
<''' ( 
DetailedRegistration''( <
>''< =
.''= >

BadRequest''> H
(''H I
	exception''I R
.''R S
Message''S Z
)''Z [
;''[ \
return(( 
response(( 
;(( 
})) 	
}** 
}++ –
KD:\Tempus\Tempus.Core\Queries\Registrations\LastUpdated\LastUpdatedQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Registrations +
.+ ,
LastUpdated, 7
;7 8
public 
class 
LastUpdatedQuery 
: 
IRequest  (
<( )
BaseResponse) 5
<5 6 
DetailedRegistration6 J
>J K
>K L
{ 
}		 ­
RD:\Tempus\Tempus.Core\Queries\Registrations\LastUpdated\LastUpdatedQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Registrations +
.+ ,
LastUpdated, 7
;7 8
public 
class #
LastUpdatedQueryHandler $
:% &
IRequestHandler' 6
<6 7
LastUpdatedQuery7 G
,G H
BaseResponseI U
<U V 
DetailedRegistrationV j
>j k
>k l
{		 
private

 
readonly

 #
IRegistrationRepository

 ,#
_registrationRepository

- D
;

D E
public 
#
LastUpdatedQueryHandler "
(" ##
IRegistrationRepository# :"
registrationRepository; Q
)Q R
{ #
_registrationRepository 
=  !"
registrationRepository" 8
;8 9
} 
public 

async 
Task 
< 
BaseResponse "
<" # 
DetailedRegistration# 7
>7 8
>8 9
Handle: @
(@ A
LastUpdatedQueryA Q
requestR Y
,Y Z
CancellationToken[ l
cancellationTokenm ~
)~ 
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
registration 
= 
await $#
_registrationRepository% <
.< =
GetLastUpdated= K
(K L
)L M
;M N
if 
( 
registration 
== 
null  $
)$ %
return& ,
BaseResponse- 9
<9 : 
DetailedRegistration: N
>N O
.O P
NotFoundP X
(X Y
$strY r
)r s
;s t
var 
response 
= 
BaseResponse '
<' ( 
DetailedRegistration( <
>< =
.= >
Ok> @
(@ A
newA D 
DetailedRegistrationE Y
{ 
Id 
= 
registration !
.! "
Id" $
,$ %
Title 
= 
registration $
.$ %
Title% *
,* +
Content 
= 
registration &
.& '
Content' .
} 
) 
; 
return   
response   
;   
}"" 	
catch## 
(## 
	Exception## 
	exception## "
)##" #
{$$ 	
var%% 
response%% 
=%% 
BaseResponse%% '
<%%' ( 
DetailedRegistration%%( <
>%%< =
.%%= >

BadRequest%%> H
(%%H I
	exception%%I R
.%%R S
Message%%S Z
)%%Z [
;%%[ \
return&& 
response&& 
;&& 
}'' 	
}(( 
}))  
>D:\Tempus\Tempus.Core\Queries\Users\GetAll\GetAllUsersQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Users #
.# $
GetAll$ *
;* +
public 
class 
GetAllUsersQuery 
: 
IRequest  (
<( )
BaseResponse) 5
<5 6
List6 :
<: ;
BaseUser; C
>C D
>D E
>E F
{ 
}		 »
ED:\Tempus\Tempus.Core\Queries\Users\GetAll\GetAllUsersQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Users #
.# $
GetAll$ *
;* +
public 
class #
GetAllUsersQueryHandler $
:% &
IRequestHandler' 6
<6 7
GetAllUsersQuery7 G
,G H
BaseResponseI U
<U V
ListV Z
<Z [
BaseUser[ c
>c d
>d e
>e f
{		 
private

 
readonly

 
IUserRepository

 $
_userRepository

% 4
;

4 5
public 
#
GetAllUsersQueryHandler "
(" #
IUserRepository# 2
userRepository3 A
)A B
{ 
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
List# '
<' (
BaseUser( 0
>0 1
>1 2
>2 3
Handle4 :
(: ;
GetAllUsersQuery; K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
users 
= 
await 
_userRepository -
.- .
GetAll. 4
(4 5
)5 6
;6 7
var 
result 
= 
BaseResponse 
< 
List !
<! "
BaseUser" *
>* +
>+ ,
., -
Ok- /
(/ 0
users0 5
.5 6
Select6 <
(< =
x= >
=>? A
newB E
BaseUserF N
(N O
xO P
.P Q
IdQ S
,S T
xU V
.V W
UserNameW _
,_ `
xa b
.b c
Emailc h
)h i
)i j
.j k
ToListk q
(q r
)r s
)s t
;t u
return 
result 
; 
} 	
catch 
( 
	Exception 
	exception "
)" #
{ 	
var 
result 
= 
BaseResponse %
<% &
List& *
<* +
BaseUser+ 3
>3 4
>4 5
.5 6

BadRequest6 @
(@ A
	exceptionA J
.J K
MessageK R
)R S
;S T
return   
result   
;   
}!! 	
}"" 
}## ‡
?D:\Tempus\Tempus.Core\Queries\Users\GetById\GetUserByIdQuery.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Users #
.# $
GetById$ +
;+ ,
public 
class 
GetUserByIdQuery 
: 
IRequest  (
<( )
BaseResponse) 5
<5 6
BaseUser6 >
>> ?
>? @
{ 
public		 

Guid		 
Id		 
{		 
get		 
;		 
init		 
;		 
}		  !
}

 Â
FD:\Tempus\Tempus.Core\Queries\Users\GetById\GetUserByIdQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Users #
.# $
GetById$ +
;+ ,
public 
class #
GetUserByIdQueryHandler $
:% &
IRequestHandler' 6
<6 7
GetUserByIdQuery7 G
,G H
BaseResponseI U
<U V
BaseUserV ^
>^ _
>_ `
{		 
private

 
readonly

 
IUserRepository

 $
_userRepository

% 4
;

4 5
public 
#
GetUserByIdQueryHandler "
(" #
IUserRepository# 2
userRepository3 A
)A B
{ 
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseUser# +
>+ ,
>, -
Handle. 4
(4 5
GetUserByIdQuery5 E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
response 
= 
await  
_userRepository! 0
.0 1
GetById1 8
(8 9
request9 @
.@ A
IdA C
)C D
;D E
if 
( 
response 
== 
null  
)  !
return" (
BaseResponse) 5
<5 6
BaseUser6 >
>> ?
.? @

BadRequest@ J
(J K
$strK [
)[ \
;\ ]
var 
result 
= 
BaseResponse %
<% &
BaseUser& .
>. /
./ 0
Ok0 2
(2 3
new3 6
BaseUser7 ?
(? @
response@ H
.H I
IdI K
,K L
responseM U
.U V
UserNameV ^
,^ _
response` h
.h i
Emaili n
)n o
)o p
;p q
return 
result 
; 
} 	
catch 
( 
	Exception 
	exception "
)" #
{ 	
var 
result 
= 
BaseResponse %
<% &
BaseUser& .
>. /
./ 0
NotFound0 8
(8 9
	exception9 B
.B C
MessageC J
)J K
;K L
return   
result   
;   
}!! 	
}"" 
}## Ô	
5D:\Tempus\Tempus.Core\Repositories\IBaseRepository.cs
	namespace 	
Tempus
 
. 
Core 
. 
Repositories "
;" #
public 
	interface 
IBaseRepository  
<  !
T! "
>" #
where 	
T
 
: 
class 
{ 
Task 
< 	
List	 
< 
T 
> 
> 
GetAll 
( 
) 
; 
Task 
< 	
T	 

?
 
> 
GetById 
( 
Guid 
id 
) 
; 
Task 
< 	
T	 

>
 
Add 
( 
T 
entity 
) 
; 
Task		 
<		 	
T			 

>		
 
Update		 
(		 
T		 
entity		 
)		 
;		 
Task

 
<

 	
Guid

	 
>

 
Delete

 
(

 
Guid

 
id

 
)

 
;

 
} ß
9D:\Tempus\Tempus.Core\Repositories\ICategoryRepository.cs
	namespace 	
Tempus
 
. 
Core 
. 
Repositories "
;" #
public 
	interface 
ICategoryRepository $
:% &
IBaseRepository' 6
<6 7
Category7 ?
>? @
{ 
Task 
< 	
List	 
< 
Category 
> 
> 
GetAll 
(  
Guid  $
userId% +
)+ ,
;, -
string 

GetCategoryColor 
( 
Guid  
id! #
)# $
;$ %
}		 …
=D:\Tempus\Tempus.Core\Repositories\IRegistrationRepository.cs
	namespace 	
Tempus
 
. 
Core 
. 
Repositories "
;" #
public 
	interface #
IRegistrationRepository (
:) *
IBaseRepository+ :
<: ;
Registration; G
>G H
{ 
Task 
< 	
List	 
< 
Registration 
> 
> 
GetAll #
(# $
Guid$ (

categoryId) 3
)3 4
;4 5
Task 
< 	
Registration	 
> 
GetLastUpdated %
(% &
)& '
;' (
}		 õ
5D:\Tempus\Tempus.Core\Repositories\IUserRepository.cs
	namespace 	
Tempus
 
. 
Core 
. 
Repositories "
;" #
public 
	interface 
IUserRepository  
:! "
IBaseRepository# 2
<2 3
User3 7
>7 8
{ 
} 