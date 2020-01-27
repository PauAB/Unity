#include "precompiledHeader.h"
#include "RenderModule.h"

bool RenderModule::init() 
{
	
	return initOpenGL();
}
void RenderModule::render()
{
	VertexDeclarationBuffer vbo(32,32);
	
	Shader vertex, fragment;

	vertex.init("Shader/data/vertexShader.vs",GL_VERTEX_SHADER);
	fragment.init("Shader/data/fragmentShader.fs",GL_FRAGMENT_SHADER);

	program = glCreateProgram();
	glAttachShader(program, vertex.getId());
	glAttachShader(program, fragment.getId());
	glLinkProgram(program);

	GLuint mvp_location, vpos_location, vcol_location;
	mvp_location = glGetUniformLocation(program, "MVP");
	vpos_location = glGetAttribLocation(program, "vPos");
	vcol_location = glGetAttribLocation(program, "vCol");


	
	glEnableVertexAttribArray(vpos_location);
	glVertexAttribPointer(vpos_location, 2, GL_FLOAT, GL_FALSE,
		sizeof(vertices), (void*)0);
	glEnableVertexAttribArray(vcol_location);
	glVertexAttribPointer(vcol_location, 3, GL_FLOAT, GL_FALSE,
		sizeof(vertices), (void*)(sizeof(float) * 2));


	while (!glfwWindowShouldClose(window->getWindowGL()))
	{
		float ratio;

		mat4x4 m, p, mvp;
		glfwGetFramebufferSize(window->getWindowGL(),
			&w,&h);
		ratio = w / (float)h;
		glViewport(0, 0, w, h);
		glClear(GL_COLOR_BUFFER_BIT);

		mat4x4_identity(m);
		mat4x4_rotate_Z(m, m, (float)glfwGetTime());
		mat4x4_ortho(p, -ratio, ratio, -1.f, 1.f, 1.f, -1.f);
		mat4x4_mul(mvp, p, m);

		glUseProgram(program);
		glUniformMatrix4fv(mvp_location, 1, GL_FALSE, (const GLfloat*)mvp);
		//glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
		glDrawArrays(GL_LINE_LOOP, 0, 3);

		glfwSwapBuffers(window->getWindowGL());
		glfwPollEvents();
	}
	destroy();
}
void RenderModule::destroy()
{
	glfwDestroyWindow(window->getWindowGL());
	glfwTerminate();
	exit(EXIT_SUCCESS);
}

RenderModule::RenderModule(WindowGLFW * w)
{
	assert(w);
	window = w;
	if (!init()) exit(EXIT_FAILURE);

}
static void key_callback(GLFWwindow* window, int key, int scancode, int action, int mods)
{


	if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
		glfwSetWindowShouldClose(window, GLFW_TRUE);
}

bool RenderModule::initOpenGL() const
{

	//TODO: ERROR CALLBACK DEVELOPMENT



	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 2);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 0);

	if (window->getWindowGL() == nullptr)
	{
		glfwTerminate();
		exit(EXIT_FAILURE);
		return false;
	}


	glfwSetKeyCallback(window->getWindowGL(),key_callback);

	glfwMakeContextCurrent(window->getWindowGL());

	GLenum err = glewInit();
	if (err != GLEW_OK)
	{
		fprintf(stderr, "Error: %s\n", glewGetErrorString(err));
		exit(EXIT_FAILURE);
	}
	glfwSwapInterval(1);


	return true;


}

RenderModule::~RenderModule()
{
}

bool RenderModule::attachShader(GLuint & shaderId)
{
	//Check if shader have been already compiled
	glAttachShader(program, shaderId);

	return true;
}

void RenderModule::useProgram()
{
	glLinkProgram(program);
}
