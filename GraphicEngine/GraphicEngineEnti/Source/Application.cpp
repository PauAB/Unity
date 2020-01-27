#include "precompiledHeader.h"
#include "../Render/RenderModule.h"

int main()
{

	WindowGLFW window(640,480,"HELLO WORLD");
	RenderModule render(&window) ;

	if (!render.init())
	{
		assert(0);
	}
	render.render();
	
}