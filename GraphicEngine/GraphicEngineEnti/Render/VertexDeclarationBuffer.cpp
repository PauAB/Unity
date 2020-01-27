#include "precompiledHeader.h"
#include "VertexDeclarationBuffer.h"


void VertexDeclarationBuffer::setVertexAttribArray() const
{

}

VertexDeclarationBuffer::VertexDeclarationBuffer(const uint32_t & nelements,
	const uint32_t & bytes_per_vertex)
{
	buffer.push_back({ 0.f,  0.6f, 0.f, 0.f, 1.f });
	buffer.push_back({ -0.6f, -0.4f, 1.f, 1.f, 0.f });
	buffer.push_back({ 0.6f, -0.4f, 0.f, 1.f, 0.f });

	/*buffer.push_back({ 0.5f, 0.5f, 1.f, 1.f, 1.f });
	buffer.push_back({ -1.0f, -1.0f, 1.f, 1.f, 1.f });
	buffer.push_back({ 1.0f, -1.0f, 1.f, 1.f, 1.f });*/

	glGenBuffers(1, &vertexBuffer);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices) * buffer.size(), buffer.data(), GL_STATIC_DRAW);

}

VertexDeclarationBuffer::~VertexDeclarationBuffer()
{
}
