<template>
	<div class="blog-album-container layout-padding">
		<ProTable ref="tableRef" :request-api="getTableList" :data-callback="dataCallBack" :columns="columns" :tool-button="false">
			<!-- v-auth="'albums:add'" -->
			<template #tools> <el-button type="primary" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #cover="{ row }">
				<el-image shape="square" :size="100" fit="cover" :src="row.cover" />
			</template>
			<template #isVisible="{ row }">
				<el-tag :type="row.isVisible ? 'success' : 'danger'"> {{ row.isVisible ? '显示' : '隐藏' }}</el-tag>
			</template>
			<template #action="{ row }">
				<!-- v-auth="'albums:edit'" -->
				<el-button icon="ele-Edit" size="small" text type="primary" @click="onOpen(row)"> 编辑 </el-button>
				<!-- v-auths="['pictures:page', 'albums:delete']" -->
				<el-dropdown>
					<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
					<template #dropdown>
						<el-dropdown-menu>
							<!-- v-if="auth('pictures:page')" -->
							<el-dropdown-item
								icon="ele-PictureFilled"
								@click="
									() => {
										router.push({
											path: '/blog/albums/pictures',
											query: {
												id: row.id,
											},
										});
									}
								"
							>
								图片列表
							</el-dropdown-item>
							<!-- v-if="auth('albums:delete')" :divided="auth('pictures:page')" -->
							<el-dropdown-item icon="ele-Delete" @click="onDeleteRole(row)"> 删除相册 </el-dropdown-item>
						</el-dropdown-menu>
					</template>
				</el-dropdown>
			</template>
		</ProTable>
		<AlbumDialog ref="albumDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="blogAlbums">
import { defineAsyncComponent, inject, reactive, ref } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
// import { auth, auths } from '@/utils/authFunction';

// 引入组件
const AlbumDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '@/components/ProTable/index.vue';
import { ColumnProps } from '@/components/ProTable/interface';
import { useRouter } from 'vue-router';
import { AlbumsPageQueryInput, AlbumsSsServiceProxy, CreateOrUpdateAlbumsInput } from '@/shared/service-proxies';
import moment from 'moment';
const _albumsService = new AlbumsSsServiceProxy(inject('$baseurl'), inject('$api'));
const albumType = [
	'首页封面图',
	'归档封面图',
	'分类封面图',
	'标签封面图',
	'相册封面图',
	'说说封面图',
	'关于封面图',
	'留言封面图',
	'个人中心封面图',
	'友情链接封面图',
	'标签列表封面图',
	'分类列表封面图',
	'打赏封面图',
];
const router = useRouter();
//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 弹窗实例
const albumDialogRef = ref<InstanceType<typeof AlbumDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '相册名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'type',
		label: '相册类型',
		search: { el: 'select' },
		enum: albumType.map((item, index) => {
			return {
				value: index,
				label: item,
			};
		}),
		width: 150,
	},
	{
		prop: 'cover',
		label: '封面',
		width: 180,
	},
	{
		prop: 'sort',
		label: '排序',
	},
	{
		prop: 'isVisible',
		label: '可见',
	},
	{
		prop: 'status',
		label: '状态',
	},
	{
		prop: 'createdTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		fixed: 'right',
		width: 150,
		// isShow: auths(['albums:edit', 'albums:delete']),
	},
]);

const dataCallBack = (data) => {
	data.rows.forEach((res) => {
		if (res.createdTime) {
			res.createdTime = moment(res.createdTime).format('YYYY-MM-DD');
		}
	});
	return data;
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params)) as AlbumsPageQueryInput;
	return _albumsService.getPage(newParams);
};
// 打开新增标签弹窗
const onOpen = (row: CreateOrUpdateAlbumsInput | null) => {
	albumDialogRef.value?.openDialog(row, albumType);
};

// 删除角色
const onDeleteRole = async (row: any) => {
	ElMessageBox.confirm(`确定删除相册：【${row.name}】吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { success } = await _albumsService.delete(row.id);
			if (success) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};
</script>
<style scoped lang="scss"></style>
