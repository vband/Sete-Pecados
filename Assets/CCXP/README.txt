> Adicione o script "ObsceneImageEffect" à Main Camera
	> Em Effect coloque o shader "ObscenePixelation"
	> Em Noise coloque o PerlinExample (CCXP > Textures)
> Adicione a ObsceneCamera na cena, e depois como filha da Main Camera
> Adicione a Layer "ObjectMask" no objeto indecente e o material ObjectMask (CCXP > Materials)
	- A Main Camera tem que renderizar todas as layers incluindo "ObjectMask" e a Obscene Camera
	tem que ter só essa layer no Culling Mask.
> Para ficar quadradinho o efeito, tem que selecionar "Full Rect" no Mesh Type nas configurações do Sprite
	