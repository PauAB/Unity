#pragma once
static const struct vertices
{
	float x, y;
	float r, g, b;
};

class VertexDeclarationBuffer
{
	GLuint vertexBuffer,posLocation,colLocation;
	std::vector<vertices> buffer;
	
	uint32_t nelements = 0;
	uint32_t bytesPerVertex = 0;
public:
	VertexDeclarationBuffer(const uint32_t & nelements,const uint32_t & bytes_per_vertex);
	void setVertexAttribArray() const;

	~VertexDeclarationBuffer();
};

