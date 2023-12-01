<template>
	<div class="system-user-container layout-padding main-box">
		<TreeFilter ref="orgTreeRef" :request-api="getTreeTableList" id="value" :default-value="initParam.orgId" @change="onChangeTree" />
		<div class="table-box">
			<ProTable
				ref="proTableRef"
				:init-param="initParam"
				:columns="columns"
				:request-api="getTableList"
				:data-callback="dataCallBack"
				:tool-button="true"
			>
				<template #tools>
					<!-- v-auth="'sysuser:add'" -->
					<el-button type="primary" icon="ele-Plus" @click="onOpenUser('')">新增</el-button>
				</template>
				<template #status="scope">
					<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
				</template>
				<template #gender="scope">
					<el-tag :type="scope.row.gender === 0 ? '' : scope.row.gender === 1 ? 'success' : 'danger'">
						{{ scope.row.gender === 0 ? '男' : scope.row.gender === 1 ? '女' : '保密' }}</el-tag
					>
				</template>
				<template #action="scope">
					<!-- v-auth="'sysuser:edit'" -->
					<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpenUser(scope.row.id)"> 编辑 </el-button>
					<!-- v-auths="['sysuser:delete', 'sysuser:reset']" -->
					<el-dropdown>
						<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
						<template #dropdown>
							<el-dropdown-menu>
								<!-- v-if="auth('sysuser:reset')" -->
								<el-dropdown-item
									icon="ele-RefreshLeft"
									@click="
										() => {
											resetDialogRef?.openDialog(scope.row.id);
										}
									"
								>
									重置密码
								</el-dropdown-item>
								<!-- v-if="auth('sysuser:delete')" -->
								<!-- :divided="auth('sysuser:reset')" -->
								<el-dropdown-item icon="ele-Delete" @click="onDeleteUser(scope.row)"> 删除账号 </el-dropdown-item>
							</el-dropdown-menu>
						</template>
					</el-dropdown>
				</template>
			</ProTable>
		</div>
		<!-- 用新增编辑 -->
		<UserDialog ref="userDialogRef" @refresh="proTableRef?.reset" />
		<!-- 密码重置 -->
		<ResetDialog ref="resetDialogRef" />
	</div>
</template>

<script setup lang="ts" name="sysUser">
import { defineAsyncComponent, reactive, ref, computed, inject } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import ProTable from '@/components/ProTable/index.vue';
import TreeFilter from '@/components/TreeFilter/index.vue';
import type { ColumnProps } from '@/components/ProTable/interface';
import { OrganizationSyssServiceProxy, QueryUserInput, TreeSelectOutput, UserSyssServiceProxy } from '@/shared/service-proxies';
import moment from 'moment';
const _userSysService = new UserSyssServiceProxy(inject('$baseurl'), inject('$api'));
const _orgSysService = new OrganizationSyssServiceProxy(inject('$baseurl'), inject('$api'));
// 引入组件
const UserDialog = defineAsyncComponent(() => import('@/views/system/user/dialog.vue'));
const ResetDialog = defineAsyncComponent(() => import('@/views/system/user/reset.vue'));

// 表单实例
const userDialogRef = ref<InstanceType<typeof UserDialog>>();
const resetDialogRef = ref<InstanceType<typeof ResetDialog>>();
//table实例
const proTableRef = ref<InstanceType<typeof ProTable>>();
const orgTreeRef = ref<InstanceType<typeof TreeFilter>>();
//机构数据
const orgs = computed(() => (orgTreeRef.value?.treeData as any) ?? ([] as TreeSelectOutput[])); //orgTreeRef.value?.treeData ?? ([] as TreeSelectOutput[]);
const initParam = reactive<{ orgId?: number | string }>({ orgId: '' });

// 机构熟选项发生改变事件
const onChangeTree = (val?: number | string) => {
	initParam.orgId = val;
};
// 表列设置
const columns: ColumnProps[] = [
	{ type: 'index', label: '序号', width: 60 },
	{
		prop: 'userName',
		label: '用户名',
		align: 'center',
		search: { el: 'input' },
	},
	{
		prop: 'name',
		label: '姓名',
		align: 'center',
		search: { el: 'input' },
	},
	{
		prop: 'nickName',
		label: '昵称',
		align: 'center',
	},
	{
		prop: 'gender',
		label: '性别',
		align: 'center',
	},
	{
		prop: 'birthday',
		label: '出生日期',
		align: 'center',
		formatter: (row: any) => {
			return row.birthday ? moment(row.birthday).format('YYYY-MM-DD') : '';
		},
	},
	{
		prop: 'mobile',
		label: '手机号码',
		align: 'center',
		search: { el: 'input' },
	},
	{
		prop: 'status',
		label: '状态',
		align: 'center',
	},
	{
		prop: 'creationTime',
		label: '创建时间',
		align: 'center',
	},
	{
		prop: 'action',
		label: '操作',
		align: 'center',
		width: 120,
		fixed: 'right',
		// isShow: auths(['sysuser:edit', 'sysuser:delete']),
	},
];

const dataCallBack = (data) => {
	data.rows.forEach((res) => {
		if (res.birthday) {
			res.birthday = moment(res.birthday).format('YYYY-MM-DD');
		}
	});
	return data;
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params)) as QueryUserInput;
	return _userSysService.pageData(newParams);
};

const getTreeTableList = (params: any) => {
	console.log(params);
	return _orgSysService.treeSelect();
};

// 打开新增用户弹窗
const onOpenUser = async (id: string) => {
	await userDialogRef.value?.openDialog(id, orgs.value);
};
// 删除用户
const onDeleteUser = async (row: any) => {
	ElMessageBox.confirm(`确定删除账号：【${row.account}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { success } = await _userSysService.delete(row.id);
			if (success) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};
</script>

<style scoped lang="scss"></style>
