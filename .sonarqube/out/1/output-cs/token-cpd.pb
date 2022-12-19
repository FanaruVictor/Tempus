Â
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
} √$
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
{o p
_categoryRepository 
= 
categoryRepository 0
;0 1
_userRepository 
= 
userRepository (
;( )
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseCategory# /
>/ 0
>0 1
Handle2 8
(8 9!
CreateCategoryCommand9 N
requestO V
,V W
CancellationToken 
cancellationToken +
)+ ,
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
user 
= 
await 
_userRepository ,
., -
GetById- 4
(4 5
request5 <
.< =
UserId= C
)C D
;D E
if 
( 
user 
== 
null 
) 
return $
BaseResponse% 1
<1 2
BaseCategory2 >
>> ?
.? @

BadRequest@ J
(J K
newK N
ListO S
<S T
stringT Z
>Z [
{[ \
$"\ ^
$str^ l
{l m
requestm t
.t u
UserIdu {
}{ |
$str	| Ü
"
Ü á
}
á à
)
à â
;
â ä
var 
entity 
= 
new 
Category %
{ 
Id 
= 
Guid 
. 
NewGuid !
(! "
)" #
,# $
Name   
=   
request   
.   
Name   #
,  # $
	CreatedAt!! 
=!! 
DateTime!! $
.!!$ %
UtcNow!!% +
,!!+ ,
LastUpdatedAt"" 
="" 
DateTime""  (
.""( )
UtcNow"") /
,""/ 0
Color## 
=## 
request## 
.##  
Color##  %
,##% &
UserId$$ 
=$$ 
request$$  
.$$  !
UserId$$! '
}%% 
;%% 
var'' 
category'' 
='' 
await''  
_categoryRepository''! 4
.''4 5
Add''5 8
(''8 9
entity''9 ?
)''? @
;''@ A
var)) 
baseCategory)) 
=)) 
GenericMapper)) ,
<)), -
Category))- 5
,))5 6
BaseCategory))7 C
>))C D
.))D E
Map))E H
())H I
category))I Q
)))Q R
;))R S
var** 
result** 
=** 
BaseResponse++ 
<++ 
BaseCategory++ )
>++) *
.++* +
Ok+++ -
(++- .
baseCategory++. :
)++: ;
;++; <
return-- 
result-- 
;-- 
}.. 	
catch// 
(// 
	Exception// 
	exception// "
)//" #
{00 	
return11 
BaseResponse11 
<11  
BaseCategory11  ,
>11, -
.11- .

BadRequest11. 8
(118 9
new119 <
List11= A
<11A B
string11B H
>11H I
{11I J
	exception11J S
.11S T
Message11T [
}11[ \
)11\ ]
;11] ^
}22 	
}33 
}44 ˙
RD:\Tempus\Tempus.Core\Commands\Categories\Create\CreateCategoryCommandValidator.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Create* 0
;0 1
public 
class *
CreateCategoryCommandValidator +
:, -
AbstractValidator/ @
<@ A!
CreateCategoryCommandA V
>V W
{ 
public 
*
CreateCategoryCommandValidator )
() *
)* +
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
UserId		 
)		 
.		 
NotNull		 &
(		& '
)		' (
;		( )
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
UserId

 
)

 
.

 
NotEqual

 '
(

' (
Guid

( ,
.

, -
Empty

- 2
)

2 3
;

3 4
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
NotEmpty %
(% &
)& '
;' (
RuleFor 
( 
x 
=> 
x 
. 
Color 
) 
. 
NotEmpty &
(& '
)' (
;( )
} 
} ó
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
}		 Í
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
;P Q
BaseResponse 
< 
Guid 
> 
result %
;% &
if 
( 
deletedCategoryId !
==" $
Guid% )
.) *
Empty* /
)/ 0
{ 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,
NotFound, 4
(4 5
$"5 7
$str7 I
{I J
requestJ Q
.Q R
IdR T
}T U
$strU _
"_ `
)` a
;a b
return 
result 
; 
} 
result   
=   
BaseResponse   !
<  ! "
Guid  " &
>  & '
.  ' (
Ok  ( *
(  * +
deletedCategoryId  + <
)  < =
;  = >
return!! 
result!! 
;!! 
}"" 	
catch## 
(## 
	Exception## 
	exception## "
)##" #
{$$ 	
var%% 
result%% 
=%% 
BaseResponse%% %
<%%% &
Guid%%& *
>%%* +
.%%+ ,

BadRequest%%, 6
(%%6 7
new%%7 :
List%%; ?
<%%? @
string%%@ F
>%%F G
{%%G H
	exception%%H Q
.%%Q R
Message%%R Y
}%%Y Z
)%%Z [
;%%[ \
return&& 
result&& 
;&& 
}'' 	
}(( 
})) …
RD:\Tempus\Tempus.Core\Commands\Categories\Delete\DeleteCategoryCommandValidator.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Delete* 0
;0 1
public 
class *
DeleteCategoryCommandValidator +
:, -
AbstractValidator. ?
<? @!
DeleteCategoryCommand@ U
>U V
{ 
public 
*
DeleteCategoryCommandValidator )
() *
)* +
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Id		 
)		 
.		 
NotNull		 "
(		" #
)		# $
;		$ %
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Id

 
)

 
.

 
NotEqual

 #
(

# $
Guid

$ (
.

( )
Empty

) .
)

. /
;

/ 0
} 
} ·
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
} ⁄!
PD:\Tempus\Tempus.Core\Commands\Categories\Update\UpdateCategoryCommandHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Update* 0
;0 1
public

 
class

 (
UpdateCategoryCommandHandler

 )
:

* +
IRequestHandler

, ;
<

; <!
UpdateCategoryCommand

< Q
,

Q R
BaseResponse

S _
<

_ `
BaseCategory

` l
>

l m
>

m n
{ 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
public 
(
UpdateCategoryCommandHandler '
(' (
ICategoryRepository( ;
categoryRepository< N
)N O
{ 
_categoryRepository 
= 
categoryRepository 0
;0 1
} 
public 

async 
Task 
< 
BaseResponse "
<" #
BaseCategory# /
>/ 0
>0 1
Handle2 8
(8 9!
UpdateCategoryCommand9 N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
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
await 
_categoryRepository 2
.2 3
GetById3 :
(: ;
request; B
.B C
IdC E
)E F
;F G
if 
( 
entity 
== 
null 
) 
return 
BaseResponse #
<# $
BaseCategory$ 0
>0 1
.1 2
NotFound2 :
(: ;
$" 
$str (
{( )
request) 0
.0 1
Id1 3
}3 4
$str4 ?
"? @
)@ A
;A B
entity 
= 
new 
Category !
{   
Id!! 
=!! 
entity!! 
.!! 
Id!! 
,!! 
Name"" 
="" 
request"" 
."" 
Name"" #
,""# $
	CreatedAt## 
=## 
entity## "
.##" #
	CreatedAt### ,
,##, -
LastUpdatedAt$$ 
=$$ 
DateTime$$  (
.$$( )
UtcNow$$) /
,$$/ 0
Color%% 
=%% 
request%% 
.%%  
Color%%  %
,%%% &
UserId&& 
=&& 
entity&& 
.&&  
UserId&&  &
}'' 
;'' 
var)) 
category)) 
=)) 
await))  
_categoryRepository))! 4
.))4 5
Update))5 ;
()); <
entity))< B
)))B C
;))C D
var++ 
baseCategory++ 
=++ 
GenericMapper++ ,
<++, -
Category++- 5
,++5 6
BaseCategory++7 C
>++C D
.++D E
Map++E H
(++H I
category++I Q
)++Q R
;++R S
var,, 
result,, 
=,, 
BaseResponse,, %
<,,% &
BaseCategory,,& 2
>,,2 3
.,,3 4
Ok,,4 6
(,,6 7
baseCategory,,7 C
),,C D
;,,D E
return.. 
result.. 
;.. 
}// 	
catch00 
(00 
	Exception00 
	exception00 "
)00" #
{11 	
var22 
result22 
=22 
BaseResponse22 %
<22% &
BaseCategory22& 2
>222 3
.223 4

BadRequest224 >
(22> ?
new22? B
List22C G
<22G H
string22H N
>22N O
{22O P
	exception22P Y
.22Y Z
Message22Z a
}22a b
)22b c
;22c d
return33 
result33 
;33 
}44 	
}55 
}66 Ú
RD:\Tempus\Tempus.Core\Commands\Categories\Update\UpdateCategoryCommandValidator.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 

Categories )
.) *
Update* 0
;0 1
public 
class *
UpdateCategoryCommandValidator +
:, -
AbstractValidator. ?
<? @!
UpdateCategoryCommand@ U
>U V
{ 
public 
*
UpdateCategoryCommandValidator )
() *
)* +
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Id		 
)		 
.		 
NotNull		 "
(		" #
)		# $
;		$ %
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Id

 
)

 
.

 
NotEqual

 #
(

# $
Guid

$ (
.

( )
Empty

) .
)

. /
;

/ 0
RuleFor 
( 
x 
=> 
x 
. 
Color 
) 
. 
NotEmpty &
(& '
)' (
;( )
RuleFor 
( 
x 
=> 
x 
. 
Name 
) 
. 
NotEmpty %
(% &
)& '
;' (
} 
} ë
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
} ”&
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
(D E
newE H
ListI M
<M N
stringN T
>T U
{U V
$"V X
$strX j
{j k
requestk r
.r s

CategoryIds }
}} ~
$str	~ à
"
à â
}
â ä
)
ä ã
;
ã å
var   
entity   
=   
new   
Registration   )
{  ) *
Id!! 
=!! 
Guid!! 
.!! 
NewGuid!! !
(!!! "
)!!" #
,!!# $
Title"" 
="" 
request"" 
.""  
Title""  %
,""% &
Content## 
=## 
request## !
.##! "
Content##" )
,##) *
	CreatedAt$$ 
=$$ 
DateTime$$ $
.$$$ %
UtcNow$$% +
,$$+ ,
LastUpdatedAt%% 
=%% 
DateTime%%  (
.%%( )
UtcNow%%) /
,%%/ 0

CategoryId&& 
=&& 
category&& %
.&&% &
Id&&& (
}'' 
;'' 
var)) 
registration)) 
=)) 
await)) $#
_registrationRepository))% <
.))< =
Add))= @
())@ A
entity))A G
)))G H
;))H I
var++  
detailedRegistration++ $
=++% &
GenericMapper++' 4
<++4 5
Registration++5 A
,++A B 
DetailedRegistration++C W
>++W X
.++X Y
Map++Y \
(++\ ]
registration++] i
)++i j
;++j k
var,, 
result,, 
=,, 
BaseResponse,, %
<,,% & 
DetailedRegistration,,& :
>,,: ;
.,,; <
Ok,,< >
(,,> ? 
detailedRegistration,,? S
),,S T
;,,T U
return.. 
result.. 
;.. 
}// 	
catch00 
(00 
	Exception00 
	exception00 "
)00" #
{11 	
var22 
result22 
=22 
BaseResponse22 %
<22% & 
DetailedRegistration22& :
>22: ;
.22; <

BadRequest22< F
(22F G
new22G J
List22K O
<22O P
string22P V
>22V W
{22W X
	exception22X a
.22a b
Message22b i
}22i j
)22j k
;22k l
return33 
result33 
;33 
}44 	
}55 
}66 õ
YD:\Tempus\Tempus.Core\Commands\Registrations\Create\CreateRegistrationCommandValidator.cs
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
Create- 3
;3 4
public 
class .
"CreateRegistrationCommandValidator /
:0 1
AbstractValidator2 C
<C D%
CreateRegistrationCommandD ]
>] ^
{ 
public 
.
"CreateRegistrationCommandValidator -
(- .
). /
{		 
RuleFor

 
(

 
x

 
=>

 
x

 
.

 

CategoryId

 !
)

! "
.

" #
NotNull

# *
(

* +
)

+ ,
;

, -
RuleFor 
( 
x 
=> 
x 
. 

CategoryId !
)! "
." #
NotEqual# +
(+ ,
Guid, 0
.0 1
Empty1 6
)6 7
;7 8
RuleFor 
( 
x 
=> 
x 
. 
Content 
) 
.  
NotEmpty  (
(( )
)) *
;* +
RuleFor 
( 
x 
=> 
x 
. 
Title 
) 
. 
NotEmpty &
(& '
)' (
;( )
} 
} •
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
}		 ’
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
;X Y
BaseResponse 
< 
Guid 
> 
result %
;% &
if 
( !
deletedRegistrationId %
==& (
Guid) -
.- .
Empty. 3
)3 4
{ 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,
NotFound, 4
(4 5
$"5 7
$str7 M
{M N
requestN U
.U V
IdV X
}X Y
$strY c
"c d
)d e
;e f
return 
result 
; 
} 
result   
=   
BaseResponse   !
<  ! "
Guid  " &
>  & '
.  ' (
Ok  ( *
(  * +!
deletedRegistrationId  + @
)  @ A
;  A B
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
<%%  
Guid%%  $
>%%$ %
.%%% &

BadRequest%%& 0
(%%0 1
new%%1 4
List%%5 9
<%%9 :
string%%: @
>%%@ A
{%%A B
	exception%%B K
.%%K L
Message%%L S
}%%S T
)%%T U
;%%U V
}&& 	
}'' 
}(( ﬂ
YD:\Tempus\Tempus.Core\Commands\Registrations\Delete\DeleteRegistrationCommandValidator.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Delete- 3
;3 4
public 
class .
"DeleteRegistrationCommandValidator /
:0 1
AbstractValidator2 C
<C D%
DeleteRegistrationCommandD ]
>] ^
{ 
public 
.
"DeleteRegistrationCommandValidator -
(- .
). /
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Id		 
)		 
.		 
NotNull		 "
(		" #
)		# $
;		$ %
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Id

 
)

 
.

 
NotEqual

 #
(

# $
Guid

$ (
.

( )
Empty

) .
)

. /
;

/ 0
} 
} â
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
} Ú"
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
.9 :
NotFound: B
(B C
$"C E
$strE [
{[ \
request\ c
.c d
Idd f
}f g
$strg u
"u v
)v w
;w x
entity 
= 
new 
Registration %
{ 
Id   
=   
entity   
.   
Id   
,   
Title!! 
=!! 
request!! 
.!!  
Title!!  %
,!!% &
Content"" 
="" 
request"" !
.""! "
Content""" )
,"") *
	CreatedAt## 
=## 
entity## "
.##" #
	CreatedAt### ,
,##, -
LastUpdatedAt$$ 
=$$ 
DateTime$$  (
.$$( )
UtcNow$$) /
,$$/ 0

CategoryId%% 
=%% 
entity%% #
.%%# $

CategoryId%%$ .
}&& 
;&& 
var(( 
registration(( 
=(( 
await(( $#
_registrationRepository((% <
.((< =
Update((= C
(((C D
entity((D J
)((J K
;((K L
var**  
detailedRegistration** $
=**% &
GenericMapper**' 4
<**4 5
Registration**5 A
,**A B 
DetailedRegistration**C W
>**W X
.**X Y
Map**Y \
(**\ ]
registration**] i
)**i j
;**j k
var++ 
result++ 
=++ 
BaseResponse++ %
<++% & 
DetailedRegistration++& :
>++: ;
.++; <
Ok++< >
(++> ? 
detailedRegistration++? S
)++S T
;++T U
return,, 
result,, 
;,, 
}-- 	
catch.. 
(.. 
	Exception.. 
	exception.. "
).." #
{// 	
var00 
result00 
=00 
BaseResponse00 %
<00% & 
DetailedRegistration00& :
>00: ;
.00; <

BadRequest00< F
(00F G
new00G J
List00K O
<00O P
string00P V
>00V W
{00W X
	exception00X a
.00a b
Message00b i
}00i j
)00j k
;00k l
return11 
result11 
;11 
}22 	
}33 
}44 ã
YD:\Tempus\Tempus.Core\Commands\Registrations\Update\UpdateRegistrationCommandValidator.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Registrations ,
., -
Update- 3
;3 4
public 
class .
"UpdateRegistrationCommandValidator /
:0 1
AbstractValidator2 C
<C D%
UpdateRegistrationCommandD ]
>] ^
{ 
public 
.
"UpdateRegistrationCommandValidator -
(- .
). /
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Id		 
)		 
.		 
NotNull		 "
(		" #
)		# $
;		$ %
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Id

 
)

 
.

 
NotEqual

 #
(

# $
Guid

$ (
.

( )
Empty

) .
)

. /
;

/ 0
RuleFor 
( 
x 
=> 
x 
. 
Content 
) 
.  
NotEmpty  (
(( )
)) *
;* +
RuleFor 
( 
x 
=> 
x 
. 
Title 
) 
. 
NotEmpty &
(& '
)' (
;( )
} 
} ´
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
} √
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
{ 
Id 
= 
Guid 
. 
NewGuid !
(! "
)" #
,# $
UserName 
= 
request "
." #
UserName# +
,+ ,
Email 
= 
request 
.  
Email  %
} 
; 
var   
user   
=   
await   
_userRepository   ,
.  , -
Add  - 0
(  0 1
entity  1 7
)  7 8
;  8 9
var"" 
baseUser"" 
="" 
GenericMapper"" (
<""( )
User"") -
,""- .
BaseUser""/ 7
>""7 8
.""8 9
Map""9 <
(""< =
user""= A
)""A B
;""B C
var## 
result## 
=## 
BaseResponse## %
<##% &
BaseUser##& .
>##. /
.##/ 0
Ok##0 2
(##2 3
baseUser##3 ;
)##; <
;##< =
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
<))% &
BaseUser))& .
>)). /
.))/ 0

BadRequest))0 :
()): ;
new)); >
List))? C
<))C D
string))D J
>))J K
{))K L
	exception))L U
.))U V
Message))V ]
}))] ^
)))^ _
;))_ `
return** 
result** 
;** 
}++ 	
},, 
}-- »
ID:\Tempus\Tempus.Core\Commands\Users\Create\CreateUserCommandValidator.cs
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
Create% +
;+ ,
public 
class &
CreateUserCommandValidator '
:( )
AbstractValidator* ;
<; <
CreateUserCommand< M
>M N
{ 
public 
&
CreateUserCommandValidator %
(% &
)& '
{		 
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
UserName

 
)

  
.

  !
NotEmpty

! )
(

) *
)

* +
;

+ ,
RuleFor 
( 
x 
=> 
x 
. 
Email 
) 
. 
NotEmpty &
(& '
)' (
.( )
Must) -
(- .

ValidEmail. 8
)8 9
;9 :
} 
private 
bool 

ValidEmail 
( 
string "
email# (
)( )
{ 
return 
new !
EmailAddressAttribute (
(( )
)) *
.* +
IsValid+ 2
(2 3
email3 8
)8 9
;9 :
} 
} Ö
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
}		 §
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
var 
deletedUserId 
= 
await  %
_userRepository& 5
.5 6
Delete6 <
(< =
request= D
.D E
IdE G
)G H
;H I
BaseResponse 
< 
Guid 
> 
result %
;% &
if 
( 
deletedUserId 
==  
Guid! %
.% &
Empty& +
)+ ,
{ 
result 
= 
BaseResponse %
<% &
Guid& *
>* +
.+ ,
NotFound, 4
(4 5
$"5 7
$str7 E
{E F
requestF M
.M N
IdN P
}P Q
$strQ [
"[ \
)\ ]
;] ^
return 
result 
; 
} 
result 
= 
BaseResponse !
<! "
Guid" &
>& '
.' (
Ok( *
(* +
deletedUserId+ 8
)8 9
;9 :
return   
result   
;   
}!! 	
catch"" 
("" 
	Exception"" 
	exception"" "
)""" #
{## 	
var$$ 
result$$ 
=$$ 
BaseResponse$$ %
<$$% &
Guid$$& *
>$$* +
.$$+ ,

BadRequest$$, 6
($$6 7
new$$7 :
List$$; ?
<$$? @
string$$@ F
>$$F G
{$$G H
	exception$$H Q
.$$Q R
Message$$R Y
}$$Y Z
)$$Z [
;$$[ \
return%% 
result%% 
;%% 
}&& 	
}'' 
}(( Ø
ID:\Tempus\Tempus.Core\Commands\Users\Delete\DeleteUserCommandValidator.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commands 
. 
Users $
.$ %
Delete% +
;+ ,
public 
class &
DeleteUserCommandValidator '
:( )
AbstractValidator* ;
<; <
DeleteUserCommand< M
>M N
{ 
public 
&
DeleteUserCommandValidator %
(% &
)& '
{ 
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Id		 
)		 
.		 
NotNull		 "
(		" #
)		# $
;		$ %
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Id

 
)

 
.

 
NotEqual

 #
(

# $
Guid

$ (
.

( )
Empty

) .
)

. /
;

/ 0
} 
} ¿
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
} ó
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
.; <
NotFound< D
(D E
$"E G
$strG T
{T U
requestU \
.\ ]
Id] _
}_ `
$str` f
"f g
)g h
;h i
user 
= 
new 
User 
{ 
Id 
= 
user 
. 
Id 
, 
UserName 
= 
request "
." #
UserName# +
,+ ,
Email 
= 
request 
.  
Email  %
}   
;   
await"" 
_userRepository"" !
.""! "
Update""" (
(""( )
user"") -
)""- .
;"". /
var$$ 
baseUser$$ 
=$$ 
GenericMapper$$ (
<$$( )
User$$) -
,$$- .
BaseUser$$/ 7
>$$7 8
.$$8 9
Map$$9 <
($$< =
user$$= A
)$$A B
;$$B C
var%% 
result%% 
=%% 
BaseResponse%% %
<%%% &
BaseUser%%& .
>%%. /
.%%/ 0
Ok%%0 2
(%%2 3
baseUser%%3 ;
)%%; <
;%%< =
return'' 
result'' 
;'' 
}(( 	
catch)) 
()) 
	Exception)) 
	exception)) "
)))" #
{** 	
return++ 
BaseResponse++ 
<++  
BaseUser++  (
>++( )
.++) *

BadRequest++* 4
(++4 5
new++5 8
List++9 =
<++= >
string++> D
>++D E
{++E F
	exception++F O
.++O P
Message++P W
}++W X
)++X Y
;++Y Z
},, 	
}-- 
}.. ü
ID:\Tempus\Tempus.Core\Commands\Users\Update\UpdateUserCommandValidator.cs
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
Update% +
;+ ,
public 
class &
UpdateUserCommandValidator '
:( )
AbstractValidator* ;
<; <
UpdateUserCommand< M
>M N
{ 
public 
&
UpdateUserCommandValidator %
(% &
)& '
{		 
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Id

 
)

 
.

 
NotNull

 "
(

" #
)

# $
;

$ %
RuleFor 
( 
x 
=> 
x 
. 
Id 
) 
. 
NotEqual #
(# $
Guid$ (
.( )
Empty) .
). /
;/ 0
RuleFor 
( 
x 
=> 
x 
. 
UserName 
)  
.  !
NotEmpty! )
() *
)* +
;+ ,
RuleFor 
( 
x 
=> 
x 
. 
Email 
) 
. 
NotEmpty &
(& '
)' (
.( )
Must) -
(- .

ValidEmail. 8
)8 9
;9 :
} 
private 
bool 

ValidEmail 
( 
string "
email# (
)( )
{ 
return 
new !
EmailAddressAttribute (
(( )
)) *
.* +
IsValid+ 2
(2 3
email3 8
)8 9
;9 :
} 
} ¶
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
=0 1
default2 9
)9 :
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
("", -
List""- 1
<""1 2
string""2 8
>""8 9
message"": A
)""A B
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
='' 
message'' 
}(( 	
;((	 

})) 
}** ñ
.D:\Tempus\Tempus.Core\Commons\GenericMapper.cs
	namespace 	
Tempus
 
. 
Core 
. 
Commons 
; 
public 
static 
class 
GenericMapper !
<! "
TSource" )
,) *
TResult+ 2
>2 3
where 	
TSource
 
: 
class 
where 	
TResult
 
: 
class 
{ 
public		 

static		 
TResult		 
Map		 
(		 
TSource		 %
source		& ,
)		, -
{

 
var 
config 
= 
new 
MapperConfiguration ,
(, -
cfg- 0
=>1 3
cfg4 7
.7 8
	CreateMap8 A
<A B
TSourceB I
,I J
TResultK R
>R S
(S T
)T U
)U V
;V W
var 
mapper 
= 
new 

AutoMapper #
.# $
Mapper$ *
(* +
config+ 1
)1 2
;2 3
return 
mapper 
. 
Map 
< 
TResult !
>! "
(" #
source# )
)) *
;* +
} 
} ﬁ
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
} ó
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

Guid 
Id 
{ 
get 
; 
set 
; 
}  
} Ø
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

string 
Name 
{ 
get 
; 
set !
;! "
}# $
=% &
$str' )
;) *
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
LastUpdatedAt !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 

string		 
?		 
Color		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

List

 
<

 
Registration

 
>

 
?

 
Registrations

 ,
{

- .
get

/ 2
;

2 3
set

4 7
;

7 8
}

9 :
public 

User 
? 
User 
{ 
get 
; 
set  
;  !
}" #
public 

Guid 
UserId 
{ 
get 
; 
set !
;! "
}# $
public 

Category 
( 
Guid 
id 
, 
string #
name$ (
,( )
DateTime* 2
	createdAt3 <
,< =
DateTime> F
lastUpdatedAtG T
,T U
stringV \
color] b
,b c
Guidd h
userIdi o
)o p
{ 
Id 

= 
id 
; 
Name 
= 
name 
; 
	CreatedAt 
= 
	createdAt 
; 
LastUpdatedAt 
= 
lastUpdatedAt %
;% &
Color 
= 
color 
; 
UserId 
= 
userId 
; 
} 
public 

Category 
( 
) 
{ 
} 
} Õ
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

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
? 
Content 
{ 
get  
;  !
set" %
;% &
}' (
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
LastUpdatedAt !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 

Guid		 

CategoryId		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 

Category

 
Category

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 

Registration 
( 
Guid 
id 
,  
string! '
title( -
,- .
string/ 5
content6 =
,= >
DateTime? G
	createdAtH Q
,Q R
DateTimeS [
lastUpdatedAt\ i
,i j
Guid 

categoryId 
) 
{ 
Id 

= 
id 
; 
Title 
= 
title 
; 
Content 
= 
content 
; 
	CreatedAt 
= 
	createdAt 
; 
LastUpdatedAt 
= 
lastUpdatedAt %
;% &

CategoryId 
= 

categoryId 
;  
} 
public 

Registration 
( 
) 
{ 
} 
} π
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

string 
UserName 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
public 

List 
< 
Registration 
> 
? 
Registrations ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 

List 
< 
Category 
> 
? 

Categories %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public

 

User

 
(

 
Guid

 
id

 
,

 
string

 
userName

  (
,

( )
string

* 0
email

1 6
)

6 7
{ 
Id 

= 
id 
; 
UserName 
= 
userName 
; 
Email 
= 
email 
; 
} 
public 

User 
( 
) 
{ 
} 
} ”
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

string 
Name 
{ 
get 
; 
set !
;! "
}# $
public 

DateTime 
LastUpdatedAt !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 

string		 
?		 
Color		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

Guid

 
UserId

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
} É
>D:\Tempus\Tempus.Core\Models\Registrations\BaseRegistration.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Registrations *
;* +
public 
class 
BaseRegistration 
: 

BaseEntity  *
{ 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
} ¢
BD:\Tempus\Tempus.Core\Models\Registrations\DetailedRegistration.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Registrations *
;* +
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
} ¥
>D:\Tempus\Tempus.Core\Models\Registrations\RegistrationInfo.cs
	namespace 	
Tempus
 
. 
Core 
. 
Models 
. 
Registrations *
;* +
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
} ˝
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

string 
UserName 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
}		 ‡
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
UserId		 
{		 
get		 
;		 
init		 #
;		# $
}		% &
}

 ê
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
("" 
GenericMapper"" )
<"") *
Category""* 2
,""2 3
BaseCategory""4 @
>""@ A
.""A B
Map""B E
)""E F
.""F G
ToList""G M
(""M N
)""N O
)""O P
;""P Q
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
(''F G
new''G J
List''K O
<''O P
string''P V
>''V W
{''W X
	exception''X a
.''a b
Message''b i
}''i j
)''j k
;''k l
return(( 
response(( 
;(( 
})) 	
}** 
}++ ù
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
 ‘
OD:\Tempus\Tempus.Core\Queries\Categories\GetById\GetCategoryByIdQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 

Categories (
.( )
GetById) 0
;0 1
public		 
class		 '
GetCategoryByIdQueryHandler		 (
:		) *
IRequestHandler		+ :
<		: ; 
GetCategoryByIdQuery		; O
,		O P
BaseResponse		Q ]
<		] ^
BaseCategory		^ j
>		j k
>		k l
{

 
private 
readonly 
ICategoryRepository (
_categoryRepository) <
;< =
public 
'
GetCategoryByIdQueryHandler &
(& '
ICategoryRepository' :
categoryRepository; M
)M N
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
(8 9 
GetCategoryByIdQuery9 M
requestN U
,U V
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
var 
category 
= 
await  
_categoryRepository! 4
.4 5
GetById5 <
(< =
request= D
.D E
IdE G
)G H
;H I
if 
( 
category 
== 
null  
)  !
return 
BaseResponse #
<# $
BaseCategory$ 0
>0 1
.1 2
NotFound2 :
(: ;
$str; P
)P Q
;Q R
var 
baseCategory 
= 
GenericMapper ,
<, -
Category- 5
,5 6
BaseCategory7 C
>C D
.D E
MapE H
(H I
categoryI Q
)Q R
;R S
var 
response 
= 
BaseResponse '
<' (
BaseCategory( 4
>4 5
.5 6
Ok6 8
(8 9
baseCategory9 E
)E F
;F G
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
(%%8 9
new%%9 <
List%%= A
<%%A B
string%%B H
>%%H I
{%%J K
	exception%%L U
.%%U V
Message%%V ]
}%%^ _
)%%_ `
;%%` a
}&& 	
}'' 
}(( Ù
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

CategoryId		 
{		 
get		 !
;		! "
init		# '
;		' (
}		) *
}

 é$
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
(44J K
new44K N
List44O S
<44S T
string44T Z
>44Z [
{44[ \
	exception44\ e
.44e f
Message44f m
}44m n
)44n o
;44o p
return55 
response55 
;55 
}66 	
}77 
}88 ≥
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
 Ì
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
if 
( 
registration 
== 
null  $
)$ %
return& ,
BaseResponse- 9
<9 : 
DetailedRegistration: N
>N O
.O P
NotFoundP X
(X Y
$strY r
)r s
;s t
var 
response 
= 
BaseResponse '
<' ( 
DetailedRegistration( <
>< =
.= >
Ok> @
(@ A
newA D 
DetailedRegistrationE Y
{ 
Id 
= 
registration !
.! "
Id" $
,$ %
Title 
= 
registration $
.$ %
Title% *
,* +
Content   
=   
registration   &
.  & '
Content  ' .
}!! 
)!! 
;!! 
return"" 
response"" 
;"" 
}## 	
catch$$ 
($$ 
	Exception$$ 
	exception$$ "
)$$" #
{%% 	
var&& 
response&& 
=&& 
BaseResponse&& '
<&&' ( 
DetailedRegistration&&( <
>&&< =
.&&= >

BadRequest&&> H
(&&H I
new&&I L
List&&M Q
<&&Q R
string&&R X
>&&X Y
{&&Y Z
	exception&&Z c
.&&c d
Message&&d k
}&&k l
)&&l m
;&&m n
return'' 
response'' 
;'' 
}(( 	
})) 
}** ¥
ZD:\Tempus\Tempus.Core\Queries\Registrations\LastUpdated\GetLastRegistrationUpdatedQuery.cs
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
class +
GetLastUpdatedRegsitrationQuery ,
:- .
IRequest/ 7
<7 8
BaseResponse8 D
<D E 
DetailedRegistrationE Y
>Y Z
>Z [
{ 
}		 Û
aD:\Tempus\Tempus.Core\Queries\Registrations\LastUpdated\GetLastRegistrationUpdatedQueryHandler.cs
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
class 2
&GetLastRegistrationUpdatedQueryHandler 3
:4 5
IRequestHandler6 E
<E F+
GetLastUpdatedRegsitrationQueryF e
,e f
BaseResponseg s
<s t!
DetailedRegistration	t à
>
à â
>
â ä
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
2
&GetLastRegistrationUpdatedQueryHandler 1
(1 2#
IRegistrationRepository2 I"
registrationRepositoryJ `
)` a
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
(@ A+
GetLastUpdatedRegsitrationQueryA `
requesta h
,h i
CancellationTokenj {
cancellationToken	| ç
)
ç é
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
(%%H I
new%%I L
List%%M Q
<%%Q R
string%%R X
>%%X Y
{%%Y Z
	exception%%Z c
.%%c d
Message%%d k
}%%k l
)%%l m
;%%m n
return&& 
response&& 
;&& 
}'' 	
}(( 
})) †
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
}		 ©
ED:\Tempus\Tempus.Core\Queries\Users\GetAll\GetAllUsersQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Users #
.# $
GetAll$ *
;* +
public		 
class		 #
GetAllUsersQueryHandler		 $
:		% &
IRequestHandler		' 6
<		6 7
GetAllUsersQuery		7 G
,		G H
BaseResponse		I U
<		U V
List		V Z
<		Z [
BaseUser		[ c
>		c d
>		d e
>		e f
{

 
private 
readonly 
IUserRepository $
_userRepository% 4
;4 5
public 
#
GetAllUsersQueryHandler "
(" #
IUserRepository# 2
userRepository3 A
)A B
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
<" #
List# '
<' (
BaseUser( 0
>0 1
>1 2
>2 3
Handle4 :
(: ;
GetAllUsersQuery; K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
users 
= 
await 
_userRepository -
.- .
GetAll. 4
(4 5
)5 6
;6 7
var 
result 
= 
BaseResponse 
< 
List !
<! "
BaseUser" *
>* +
>+ ,
., -
Ok- /
(/ 0
users0 5
.5 6
Select6 <
(< =
GenericMapper= J
<J K
UserK O
,O P
BaseUserP X
>X Y
.Y Z
MapZ ]
)] ^
.^ _
ToList_ e
(e f
)f g
)g h
;h i
return 
result 
; 
} 	
catch 
( 
	Exception 
	exception "
)" #
{ 	
var   
result   
=   
BaseResponse   %
<  % &
List  & *
<  * +
BaseUser  + 3
>  3 4
>  4 5
.  5 6

BadRequest  6 @
(  @ A
new  A D
List  E I
<  I J
string  J P
>  P Q
{  Q R
	exception  R [
.  [ \
Message  \ c
}  c d
)  d e
;  e f
return!! 
result!! 
;!! 
}"" 	
}## 
}$$ á
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
 Ω
FD:\Tempus\Tempus.Core\Queries\Users\GetById\GetUserByIdQueryHandler.cs
	namespace 	
Tempus
 
. 
Core 
. 
Queries 
. 
Users #
.# $
GetById$ +
;+ ,
public		 
class		 #
GetUserByIdQueryHandler		 $
:		% &
IRequestHandler		' 6
<		6 7
GetUserByIdQuery		7 G
,		G H
BaseResponse		I U
<		U V
BaseUser		V ^
>		^ _
>		_ `
{

 
private 
readonly 
IUserRepository $
_userRepository% 4
;4 5
public 
#
GetUserByIdQueryHandler "
(" #
IUserRepository# 2
userRepository3 A
)A B
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
GetUserByIdQuery5 E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 
try 
{ 	
cancellationToken 
. (
ThrowIfCancellationRequested :
(: ;
); <
;< =
var 
user 
= 
await 
_userRepository ,
., -
GetById- 4
(4 5
request5 <
.< =
Id= ?
)? @
;@ A
if 
( 
user 
== 
null 
) 
return $
BaseResponse% 1
<1 2
BaseUser2 :
>: ;
.; <
NotFound< D
(D E
$strE V
)V W
;W X
var 
baseUser 
= 
GenericMapper (
<( )
User) -
,- .
BaseUser/ 7
>7 8
.8 9
Map9 <
(< =
user= A
)A B
;B C
var 
result 
= 
BaseResponse %
<% &
BaseUser& .
>. /
./ 0
Ok0 2
(2 3
baseUser3 ;
); <
;< =
return 
result 
; 
} 	
catch   
(   
	Exception   
	exception   "
)  " #
{!! 	
var"" 
result"" 
="" 
BaseResponse"" %
<""% &
BaseUser""& .
>"". /
.""/ 0

BadRequest""0 :
("": ;
new""; >
List""? C
<""C D
string""D J
>""J K
{""K L
	exception""L U
.""U V
Message""V ]
}""] ^
)""^ _
;""_ `
return## 
result## 
;## 
}$$ 	
}%% 
}&& ‘	
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
} ﬂ
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
}		 Ö
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
}		 ı
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