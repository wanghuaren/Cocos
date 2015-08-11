#include "CppLua.h"

CppLua::CppLua(){
}
CppLua::~CppLua(){
}

void CppLua::callCppFunction(lua_State* ls){
	/*
	Lua���õ�C++�ĺ��������Ǿ�̬��
	*/
	lua_register(ls, "cppFunction", cppFunction);
}

int CppLua::cppFunction(lua_State* ls){
	int luaNum = (int)lua_tonumber(ls, 1);
	int luaStr = (int)lua_tostring(ls, 2);
	CCLOG("Lua����cpp����ʱ���������������� %i  %s", luaNum, luaStr);

	/*
	����Lua��ֵ
	*/
	lua_pushnumber(ls, 321);
	lua_pushstring(ls, "Himi");

	//    GameHttp * request = GameHttp::create();
	//    request->testHttp();

	/*
	����Luaֵ����
	*/
	return 2;
}

Node* CppLua::callLuaNode(string _sceneName, int _index, bool _bool){
	//lua_State*  ls = CCLuaEngine::defaultEngine()->getLuaStack()->getLuaState();
	lua_State*  ls = LuaEngine::getInstance()->getLuaStack()->getLuaState();
	//
	//    int isOpen = luaL_dofile(ls, getFileFullPath(luaFileName));
	//    if(isOpen!=0){
	//        CCLOG("Open Lua Error: %i", isOpen);
	//        return NULL;
	//    }

	lua_getglobal(ls, "getNode");

	lua_pushstring(ls, _sceneName.c_str());
	lua_pushnumber(ls, _index);
	lua_pushboolean(ls, _bool);

	/*
	lua_call
	��һ������:�����Ĳ�������
	�ڶ�������:��������ֵ����
	*/
	lua_call(ls, 3, 1);

	Node* nResult = *(Node**)lua_touserdata(ls, -1);
	return nResult;
}

ActionTimeline* CppLua::callLuaAnima(string _sceneName, int _index, bool _bool){
	//lua_State*  ls = CCLuaEngine::defaultEngine()->getLuaStack()->getLuaState();
	lua_State*  ls = LuaEngine::getInstance()->getLuaStack()->getLuaState();
	//
	//    int isOpen = luaL_dofile(ls, getFileFullPath(luaFileName));
	//    if(isOpen!=0){
	//        CCLOG("Open Lua Error: %i", isOpen);
	//        return NULL;
	//    }

	lua_getglobal(ls, "getAnima");

	lua_pushstring(ls, _sceneName.c_str());
	lua_pushnumber(ls, _index);
	lua_pushboolean(ls, _bool);

	/*
	lua_call
	��һ������:�����Ĳ�������
	�ڶ�������:��������ֵ����
	*/
	lua_call(ls, 3, 1);

	ActionTimeline* nResult = *(ActionTimeline**)lua_touserdata(ls, -1);
	return nResult;
}
